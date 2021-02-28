// Shader�u���b�N
// �V�F�[�_�[�̏����L�q����
Shader "Unlit/UnlitShaderTemplate"
{
    // Properties�u���b�N
    // Unity��ł���������v���p�e�B�����L�q����
    // �}�e���A����Inspector�E�B���h�E��ɕ\������A�X�N���v�g�ォ����ݒ�ł���
    Properties
    {
        // �v���p�e�B�� ("Inspector�ɕ\�����閼�O", �^) = "�f�t�H���g�l" { �I�v�V���� }
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
    }

    // SubShader�u���b�N
    // �V�F�[�_�[�̎�ȏ����͂��̒��ɋL�q����
    // �T�u�V�F�[�_�[�͕����������Ƃ��\���A��{�͈��
    SubShader
    {
        // Unity���ɃV�F�[�_�[�̐ݒ��`���邽�߂ɕt����^�O�̂���
        Tags
        {
            // �����_�����O�^�C�v
            "RenderType" = "Opaque" // �������Ȃ�Transparent�A����ȊO�Ȃ�Opaque
            // �����_�[�L���[�F�I�u�W�F�N�g�̕`�揇�𐧌�i���������������̂��珇�Ԃɕ`�悳���j
            //"Queue" = "Geometry+10"
        }
        // Level of Detail�F�T�u�V�F�[�_�[���g�������邽�߂̎d�g�݂ŁA�������l�̖������ʂ���
        LOD 100
        // Z�e�X�g(�f�v�X�e�X�g)
        //ZTest Always

        // Pass�u���b�N
        // 1�̃I�u�W�F�N�g��1�x�̕`��ōs�������������ɏ���
        // �������{������A���G�ȕ`�������Ƃ��͕����������Ƃ��\
        Pass
        {
            // �v���O�����������n�߂�Ƃ����錾
            CGPROGRAM // Cg(C for graphics)����
            // HLSLPROGRAM // HLSL����FDirectX�Ŏg�p�����
            // GLSLPROGRAM // GLSL����FGLSL�Ŏg�p�����

            // �֐��錾
            #pragma vertex vert // "vert" �֐��𒸓_�V�F�[�_�[�Ƃ��Ďg�p����錾
            #pragma fragment frag // "frag" �֐����t���O�����g�V�F�[�_�[�Ƃ��Ďg�p����錾
            // make fog work
            #pragma multi_compile_fog // multi_compile����n�܂�̂̓V�F�[�_�[�o���A���g�Ƃ����@�\���g�p���邽�߂̖���

            // �w�肵���t�@�C���̒��g�����̂܂܂��̈ʒu�ɓ\��t���閽��
            #include "UnityCG.cginc"

            // ���_�V�F�[�_�[�֓��͂��钸�_�f�[�^��`
            struct appdata
            {
                float4 vertex : POSITION; // ���_���W
                float2 uv : TEXCOORD0; // �e�N�X�`��UV
            };

            // ���_�V�F�[�_�[����t���O�����g�V�F�[�_�[�֎󂯓n���f�[�^�̒�`
            struct v2f
            {
                float2 uv : TEXCOORD0; // �e�N�X�`��UV
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION; // ���W�ϊ����ꂽ��̒��_���W
            };

            // �O���[�o���ϐ��̐錾
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            // ���_�V�F�[�_�[
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // 3D��Ԃ���X�N���[����̈ʒu�ւ̍��W�ϊ�
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // �e�N�X�`����UV�l���v�Z���鏈��
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            // �t���O�����g�V�F�[�_�[
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color; // �w�肵���e�N�X�`������A�w��UV�l�̐F�����o�������i�e�N�X�`���T���v�����O�j
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            // �v���O�����������I���Ƃ����錾
            ENDCG // Gg����
            // ENDHLSL // HLSL����
            // ENDGLSL // GLSL����
        }
    }
}
