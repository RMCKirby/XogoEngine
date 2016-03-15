using System;
using System.Runtime.InteropServices;
using XogoEngine.OpenGL.Utilities;

namespace XogoEngine.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Colour4 : IEquatable<Colour4>
    {
        public Colour4(float red, float green, float blue)
            : this(red, green, blue, 255)
        {
        }

        public Colour4(float red, float green, float blue, float alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        public float R { get; }
        public float G { get; }
        public float B { get; }
        public float A { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Colour4))
            {
                return false;
            }
            return Equals((Colour4)obj);
        }

        public bool Equals(Colour4 other)
        {
            return R == other.R &&
                   G == other.G &&
                   B == other.B &&
                   A == other.A;
        }

        public static bool operator ==(Colour4 left, Colour4 right) => left.Equals(right);
        public static bool operator !=(Colour4 left, Colour4 right) => !left.Equals(right);

        public override int GetHashCode()
            => HashCodeGenerator.Initialise().Hash(R).Hash(G).Hash(B).Hash(A).Value;

        public override string ToString()
        {
            return $"[Colour4 : R={R}, G={G}, B={B}, A={A}]";
        }

        /***********************************************
             **************** BASIC Colours ****************/
        public static readonly Colour4 Black = new Colour4(0, 0, 0, 0);
        public static readonly Colour4 White = new Colour4(255, 255, 255);
        public static readonly Colour4 Grey = new Colour4(128, 128, 128);
        public static readonly Colour4 Silver = new Colour4(192, 192, 192);
        public static readonly Colour4 Red = new Colour4(255, 0, 0);
        public static readonly Colour4 Green = new Colour4(0, 128, 0);
        public static readonly Colour4 Blue = new Colour4(0, 0, 255);
        public static readonly Colour4 Yellow = new Colour4(255, 255, 0);
        public static readonly Colour4 Orange = new Colour4(255, 165, 0);
        public static readonly Colour4 Purple = new Colour4(128, 0, 128);
        public static readonly Colour4 Indigo = new Colour4(75, 0, 130);
        public static readonly Colour4 Lime = new Colour4(0, 255, 0);
        public static readonly Colour4 Magenta = new Colour4(255, 0, 255);
        public static readonly Colour4 Maroon = new Colour4(128, 0, 0);
        public static readonly Colour4 Olive = new Colour4(128, 128, 0);
        public static readonly Colour4 Teal = new Colour4(0, 128, 128);

        /***********************************************
             ******************** BLUES ********************/
        public static readonly Colour4 MidnightBlue = new Colour4(25, 25, 112);
        public static readonly Colour4 Navy = new Colour4(0, 0, 128);
        public static readonly Colour4 CornFlowerBlue = new Colour4(100, 149, 237);
        public static readonly Colour4 DarkSlateBlue = new Colour4(72, 61, 139);
        public static readonly Colour4 SlateBlue = new Colour4(106, 90, 205);
        public static readonly Colour4 MediumSlateBlue = new Colour4(123, 104, 238);
        public static readonly Colour4 LightSlateBlue = new Colour4(132, 112, 255);
        public static readonly Colour4 MediumBlue = new Colour4(0, 0, 205);
        public static readonly Colour4 RoyalBlue = new Colour4(65, 105, 225);
        public static readonly Colour4 DodgerBlue = new Colour4(30, 144, 255);
        public static readonly Colour4 DeepSkyBlue = new Colour4(0, 191, 255);
        public static readonly Colour4 SkyBlue = new Colour4(135, 206, 235);
        public static readonly Colour4 LightSkyBlue = new Colour4(135, 196, 250);
        public static readonly Colour4 SteelBlue = new Colour4(70, 130, 180);
        public static readonly Colour4 LightSteelBlue = new Colour4(176, 196, 222);
        public static readonly Colour4 LightBlue = new Colour4(173, 216, 230);
        public static readonly Colour4 PowderBlue = new Colour4(176, 24, 230);
        public static readonly Colour4 PaleTurquoise = new Colour4(175, 238, 238);
        public static readonly Colour4 DarkTurquoise = new Colour4(0, 206, 209);
        public static readonly Colour4 MediumTurquoise = new Colour4(72, 209, 204);
        public static readonly Colour4 Turquoise = new Colour4(64, 224, 208);
        public static readonly Colour4 Cyan = new Colour4(0, 255, 255);
        public static readonly Colour4 LightCyan = new Colour4(224, 255, 255);
        public static readonly Colour4 CadetBlue = new Colour4(95, 158, 160);

        /***********************************************
             ******************** GREENS *******************/
        public static readonly Colour4 MediumAquamarine = new Colour4(102, 205, 170);
        public static readonly Colour4 Aquamarine = new Colour4(127, 255, 212);
        public static readonly Colour4 DarkGreen = new Colour4(0, 100, 0);
        public static readonly Colour4 DarkOliveGreen = new Colour4(85, 107, 47);
        public static readonly Colour4 DarkSeaGreen = new Colour4(143, 188, 143);
        public static readonly Colour4 SeaGreen = new Colour4(46, 139, 87);
        public static readonly Colour4 LightSeaGreen = new Colour4(32, 178, 170);
        public static readonly Colour4 PaleGreen = new Colour4(152, 251, 152);
        public static readonly Colour4 SpringGreen = new Colour4(0, 255, 127);
        public static readonly Colour4 LawnGreen = new Colour4(124, 252, 0);
        public static readonly Colour4 Chartreuse = new Colour4(127, 255, 0);
        public static readonly Colour4 MediumSpringGreen = new Colour4(0, 250, 154);
        public static readonly Colour4 GreenYellow = new Colour4(173, 255, 47);
        public static readonly Colour4 LimeGreen = new Colour4(50, 205, 50);
        public static readonly Colour4 YellowGreen = new Colour4(154, 205, 50);
        public static readonly Colour4 ForestGreen = new Colour4(34, 139, 34);
        public static readonly Colour4 OliveDrab = new Colour4(107, 142, 35);
        public static readonly Colour4 DarkKhaki = new Colour4(189, 183, 107);
        public static readonly Colour4 Khaki = new Colour4(240, 230, 140);

        /***********************************************
             ******************** YELLOWS ******************/
        public static readonly Colour4 PaleGoldenrod = new Colour4(238, 232, 170);
        public static readonly Colour4 LightGoldenrodYellow = new Colour4(250, 250, 210);
        public static readonly Colour4 LightYellow = new Colour4(255, 255, 224);
        public static readonly Colour4 Gold = new Colour4(255, 215, 0);
        public static readonly Colour4 LightGoldenrod = new Colour4(238, 221, 130);
        public static readonly Colour4 Goldenrod = new Colour4(218, 165, 32);
        public static readonly Colour4 DarkGoldenrod = new Colour4(184, 134, 11);

        /***********************************************
             ******************** ORANGES ******************/
        public static readonly Colour4 DarkSalmon = new Colour4(233, 150, 122);
        public static readonly Colour4 Salmon = new Colour4(250, 128, 114);
        public static readonly Colour4 LightSalmon = new Colour4(255, 160, 122);
        public static readonly Colour4 DarkOrange = new Colour4(255, 140, 0);
        public static readonly Colour4 Coral = new Colour4(255, 127, 80);
        public static readonly Colour4 LightCoral = new Colour4(240, 128, 128);
        public static readonly Colour4 BlazeOrange = new Colour4(255, 102, 0);
        public static readonly Colour4 BurntOrange = new Colour4(204, 85, 0);
        public static readonly Colour4 Tomato = new Colour4(255, 99, 71);
        public static readonly Colour4 OrangeRed = new Colour4(255, 69, 0);

        /***********************************************
             ******************** BROWNS *******************/
        public static readonly Colour4 Brown = new Colour4(165, 42, 42);
        public static readonly Colour4 RosyBrown = new Colour4(188, 143, 143);
        public static readonly Colour4 IndianRed = new Colour4(205, 92, 92);
        public static readonly Colour4 SaddleBrown = new Colour4(139, 69, 19);
        public static readonly Colour4 Sienna = new Colour4(160, 82, 45);
        public static readonly Colour4 Peru = new Colour4(205, 133, 63);
        public static readonly Colour4 Burlywood = new Colour4(222, 184, 135);
        public static readonly Colour4 Beige = new Colour4(245, 245, 220);
        public static readonly Colour4 Wheat = new Colour4(245, 222, 179);
        public static readonly Colour4 SandyBrown = new Colour4(244, 164, 96);
        public static readonly Colour4 Tan = new Colour4(210, 180, 140);
        public static readonly Colour4 Chocolate = new Colour4(210, 105, 30);
        public static readonly Colour4 Firebrick = new Colour4(178, 34, 34);

        /***********************************************
             ***************** PINKS/VIOLETS ***************/
        public static readonly Colour4 Pink = new Colour4(255, 192, 203);
        public static readonly Colour4 Violet = new Colour4(238, 130, 238);
        public static readonly Colour4 HotPink = new Colour4(255, 105, 180);
        public static readonly Colour4 DeepPink = new Colour4(255, 20, 147);
        public static readonly Colour4 LightPink = new Colour4(255, 182, 193);
        public static readonly Colour4 PaleVioletRed = new Colour4(219, 112, 147);
        public static readonly Colour4 MediumVioletRed = new Colour4(199, 21, 133);
        public static readonly Colour4 VioletRed = new Colour4(208, 32, 144);
        public static readonly Colour4 Plum = new Colour4(221, 160, 221);
        public static readonly Colour4 Orchid = new Colour4(218, 112, 214);
        public static readonly Colour4 MediumOrchid = new Colour4(186, 85, 211);
        public static readonly Colour4 DarkOrchid = new Colour4(153, 50, 204);
        public static readonly Colour4 DarkViolet = new Colour4(148, 0, 211);
        public static readonly Colour4 BlueViolet = new Colour4(138, 43, 226);
        public static readonly Colour4 MediumPurple = new Colour4(147, 112, 219);
        public static readonly Colour4 Thistle = new Colour4(216, 191, 216);

        /***********************************************
             ***************** WHITES/PASTELS **************/
        public static readonly Colour4 Snow = new Colour4(255, 250, 250);
        public static readonly Colour4 GhostWhite = new Colour4(248, 248, 255);
        public static readonly Colour4 WhiteSmoke = new Colour4(245, 245, 245);
        public static readonly Colour4 Gainsboro = new Colour4(220, 220, 220);
        public static readonly Colour4 FloralWhite = new Colour4(255, 250, 240);
        public static readonly Colour4 OldLace = new Colour4(253, 245, 230);
        public static readonly Colour4 Linen = new Colour4(240, 240, 230);
        public static readonly Colour4 AntiqueWhite = new Colour4(250, 235, 215);
        public static readonly Colour4 PapayaWhip = new Colour4(255, 239, 213);
        public static readonly Colour4 BlanchedAlmond = new Colour4(255, 235, 205);
        public static readonly Colour4 Bisque = new Colour4(255, 228, 196);
        public static readonly Colour4 PeachPuff = new Colour4(255, 218, 185);
        public static readonly Colour4 NavajoWhite = new Colour4(255, 222, 173);
        public static readonly Colour4 Moccasin = new Colour4(255, 228, 181);
        public static readonly Colour4 Cornsilk = new Colour4(255, 248, 220);
        public static readonly Colour4 Ivory = new Colour4(255, 255, 240);
        public static readonly Colour4 LemonChiffon = new Colour4(255, 250, 205);
        public static readonly Colour4 Seashell = new Colour4(255, 245, 238);
        public static readonly Colour4 HoneyDew = new Colour4(240, 255, 240);
        public static readonly Colour4 MintCream = new Colour4(245, 255, 250);
        public static readonly Colour4 Azure = new Colour4(240, 255, 255);
        public static readonly Colour4 AliceBlue = new Colour4(240, 248, 255);
        public static readonly Colour4 Lavender = new Colour4(230, 230, 250);
        public static readonly Colour4 LavenderBlush = new Colour4(255, 240, 245);
        public static readonly Colour4 MistyRose = new Colour4(255, 228, 225);
    }
}
