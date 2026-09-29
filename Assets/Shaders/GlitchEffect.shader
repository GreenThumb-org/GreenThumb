Shader "Custom/StunEffect"
{
    Properties
    {
        [HideInInspector] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _EffectScale ("Effect Scale", Float) = 1.0

        [Header(Stun)]
        _StunIntensity ("Stun Intensity", Range(0, 1)) = 0.0
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0.45

        [Header(Block Glitch)]
        _BlockFrequency ("Block Frequency", Float) = 20.0
        _BlockSpeed ("Block Speed", Float) = 16.0
        _BlockDisplacement ("Block Displacement", Float) = 0.14

        [Header(Chromatic)]
        _ChromaticSpread ("Chromatic Spread", Float) = 0.08

        [Header(Neon)]
        _NeonCycleSpeed ("Neon Cycle Speed", Float) = 1.8

        [Header(Scanlines)]
        _ScanlineStrength ("Scanline Strength", Range(0, 1)) = 0.4

    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalPipeline" "IgnoreProjector"="True" }
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Stencil
        {
            Ref 2
            Comp NotEqual
            Pass Replace
        }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color      : COLOR;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 color      : COLOR;
                float2 uv         : TEXCOORD0;
            };

            sampler2D _MainTex;
            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float  _StunIntensity;
                float  _DissolveAmount;
                float  _BlockFrequency;
                float  _BlockSpeed;
                float  _BlockDisplacement;
                float  _ChromaticSpread;
                float  _NeonCycleSpeed;
                float  _ScanlineStrength;
                float _EffectScale;
            CBUFFER_END

            float randomNoise(float2 seed)
            {
                return frac(sin(dot(seed, float2(12.9898, 78.233))) * 43758.5453);
            }

            float3 RGBtoHSV(float3 rgb)
            {
                float4 k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = lerp(float4(rgb.bg, k.wz), float4(rgb.gb, k.xy), step(rgb.b, rgb.g));
                float4 q = lerp(float4(p.xyw, rgb.r), float4(rgb.r, p.yzx), step(p.x, rgb.r));
                float  d = q.x - min(q.w, q.y);
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + 1e-10)), d / (q.x + 1e-10), q.x);
            }

            float3 HSVtoRGB(float3 hsv)
            {
                float4 k = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(hsv.xxx + k.xyz) * 6.0 - k.www);
                return hsv.z * lerp(k.xxx, saturate(p - k.xxx), hsv.y);
            }

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv         = input.uv;
                output.color      = input.color * _Color;
                return output;
            }

            float4 frag(Varyings input) : SV_Target
            {
                float2 uv = input.uv;

                float horizontalBlockRow = floor(uv.y * _BlockFrequency * _EffectScale);
                float verticalBlockCol   = floor(uv.x * _BlockFrequency * 0.45 * _EffectScale);
                float timeStep           = floor(_Time.y * _BlockSpeed);

                float hBlockNoise = randomNoise(float2(horizontalBlockRow, timeStep));
                float vBlockNoise = randomNoise(float2(verticalBlockCol,   timeStep * 1.37 + 5.5));

                float hTear = step(0.78, hBlockNoise) * (hBlockNoise - 0.5) * _BlockDisplacement * _StunIntensity;
                float vTear = step(0.82, vBlockNoise) * (vBlockNoise - 0.5) * _BlockDisplacement * 0.55 * _StunIntensity;

                float2 tornUV = uv + float2(hTear, vTear);

                float spread      = _ChromaticSpread * _StunIntensity * (0.6 + saturate(hBlockNoise) * 0.8);
                float r           = tex2D(_MainTex, tornUV + float2(spread,  0)).r;
                float g           = tex2D(_MainTex, tornUV).g;
                float b           = tex2D(_MainTex, tornUV - float2(spread,  0)).b;
                float spriteAlpha = tex2D(_MainTex, tornUV).a;

                float3 sampledRGB = float3(r, g, b);

                float3 hsv               = RGBtoHSV(sampledRGB);
                float  cyclingHue        = frac(hsv.x + _Time.y * _NeonCycleSpeed * 0.15);
                float  forcedSaturation  = min(hsv.y * 2.5, 1.0);
                float3 neonCorrupted     = HSVtoRGB(float3(cyclingHue, forcedSaturation, min(hsv.z * 1.1, 1.0)));

                float3 upwardScanNeon = HSVtoRGB(float3(frac(_Time.y * _NeonCycleSpeed * 0.28 + uv.y * 0.4 * _EffectScale), 1.0, 1.0));
                float  scanBandPhase  = frac(uv.y * 2.5 * _EffectScale - _Time.y * _NeonCycleSpeed * 0.35);
                float  scanBandVisible   = smoothstep(0.88, 1.0, scanBandPhase)
                                         * step(0.55, randomNoise(float2(floor(_Time.y * 7.0), 2.71)))
                                         * _StunIntensity;

                float  corruptedBlockMask = step(0.08, hBlockNoise) * _StunIntensity;
                float3 colorizedRGB       = lerp(sampledRGB, lerp(neonCorrupted, upwardScanNeon, scanBandVisible), corruptedBlockMask);

                float cyanDataLine = step(0.972, randomNoise(float2(floor(uv.y * 100.0 * _EffectScale), floor(_Time.y * 26.0))));
                colorizedRGB         = lerp(colorizedRGB, float3(0.0, 1.0, 0.94), cyanDataLine * _StunIntensity);

                float scanlineWave = sin((uv.y - _Time.y * 0.65) * 68.0 * _EffectScale) * 0.5 + 0.5;
                float scanlineDimming = lerp(1.0, scanlineWave * 0.75 + 0.25, _ScanlineStrength * _StunIntensity);

                float dissolveNoise = randomNoise(floor(uv * 78.0 * _EffectScale) * 0.013 + floor(_Time.y * 11.0) * 0.031);
                float dissolveAlpha = step(dissolveNoise, 1.0 - _DissolveAmount * _StunIntensity);

                float flickerNoise = randomNoise(float2(floor(_Time.y * 21.0), 1.618));
                float flicker      = lerp(1.0, step(0.09, flickerNoise), _StunIntensity * 0.6);

                float4 finalColor;
                finalColor.rgb = colorizedRGB * scanlineDimming * input.color.rgb;
                finalColor.a   = spriteAlpha * dissolveAlpha * flicker * input.color.a;

                clip(finalColor.a - 0.001);
                return finalColor;
            }
            ENDHLSL
        }
    }
}