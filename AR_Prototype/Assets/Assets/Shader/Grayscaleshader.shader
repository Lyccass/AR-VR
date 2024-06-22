Shader "Custom/Grayscale" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {} // Die Haupttextur des Materials
        _GrayscaleIntensity ("Grayscale Intensity", Range(0, 1)) = 1 // Die Intensit�t der Graustufenanpassung (von 0 bis 1)
    }
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"} // Tags f�r die Render-Engine
        ZWrite Off // Deaktiviert das Schreiben in den Z-Buffer
        Blend SrcAlpha OneMinusSrcAlpha // Festlegt, wie das Material mit dem Hintergrund vermischt wird
        Pass {
            CGPROGRAM
            #pragma vertex vert // Vertex-Shader-Funktion
            #pragma fragment frag // Fragment-Shader-Funktion
            #include "UnityCG.cginc" // Enth�lt n�tzliche Funktionen und Definitionen f�r Shader
            
            // Definition einer Struktur f�r die Vertexdaten
            struct appdata_t {
                float4 vertex : POSITION; // Vertex-Position
                float2 uv : TEXCOORD0; // Texturkoordinaten
            };
            
            // Definition einer Struktur f�r die Ausgabedaten des Vertex-Shaders
            struct v2f {
                float2 uv : TEXCOORD0; // Texturkoordinaten
                float4 vertex : SV_POSITION; // Vertex-Position im Raum
            };
            
            sampler2D _MainTex; // Sampler f�r die Haupttextur
            float4 _MainTex_ST; // Textur-Transformationen (Skalierung, Verschiebung)
            float _GrayscaleIntensity; // Intensit�t der Graustufenanpassung
            
            // Vertex-Shader-Funktion, konvertiert Vertex-Koordinaten in Bildschirmkoordinaten
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Konvertiert die Vertex-Position
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // Transformiert die Texturkoordinaten
                return o;
            }
            
            // Fragment-Shader-Funktion, berechnet die Farbe jedes Pixels
            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv); // Liest die Farbe aus der Haupttextur
              
                // Konvertiert die Farbe in Graustufen
               float gray = dot(col.rgb, float3(0.299, 0.587, 0.114)); // Berechnet den Grauwert
                float3 grayscaleColor = float3(gray, gray, gray); // Konvertiert in Graustufen
                
                // Ger�teunabh�ngige Farben verwenden
                float3 normalColor = col.rgb; // Normale Farbe aus der Textur
                
                // Anpassung f�r das Mobiltelefon: Reduziere den roten Farbanteil
               
                
                // Mische zwischen Graustufen und normaler Farbe basierend auf Intensit�t
                float3 finalColor = lerp(grayscaleColor, normalColor, _GrayscaleIntensity);
                
                return fixed4(finalColor, col.a); // Gibt die resultierende Farbe zur�ck 
            }
            ENDCG
        }
    }
    Fallback "Diffuse" // Fallback-Materialtyp, wenn der Shader nicht unterst�tzt wird
}
