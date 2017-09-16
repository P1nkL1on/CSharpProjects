using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife91
{
    struct Consts
    {
        public const short max_width = 60;
        public const short max_height = 80;
        public static int point_wid = 0, point_hei = 0;
    }
    class Field
    {
        /// <summary>
        /// Количество точек, которые изменили свое состояние за последний ход
        /// </summary>
        int changed;
        int changed_last;
        /// <summary>
        ///  узор не менялся уже ... ходов
        /// </summary>
        short not_changed_for;
        int turn;
        int height;
        int width;

        // viewing parameters
        int zoom;
        int fromWid;
        int fromHei;

        Point[,] points;

        public void set_point(int X, int Y, bool life)
        {
            points[bh(X), bw(Y)] = new Point(life);
        }
        public bool get_point(int X, int Y)
        {
            return points[bh(X), bw(Y)].Life;
        }

        public Field(Random rnd, int heigth, int width, bool random_generate)
        {
            this.fromHei = 0;
            this.fromWid = 0;
            this.zoom = 1;
            this.turn = 0;
            this.changed = 1;
            this.not_changed_for = 0;
            this.height = heigth;
            this.width = width;
            points = new Point[heigth, width];
            // set them all
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (!random_generate)
                        points[i, j] = new Point(false);
                    else
                        points[i, j] = new Point((rnd.Next(Math.Abs(i + j - width / 2 - height / 2)) == 0) ? true : false);
        }

        int bw(int X)
        {
            if (X < 0) return width + X;
            if (X >= width) return X - width;
            return X;
        }
        int bh(int X)
        {
            if (X < 0) return height + X;
            if (X >= height) return X - height;
            return X;
        }

        public void UpdateMap()
        {
            turn++; changed_last = changed; changed = 0;
            // calculate
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    points[i, j].Calculate(new Point[] {     points[bh(i-1), bw(j-1)], points[bh(i-1), j], points[bh(i-1), bw(j+1)],
                                                             points[i, bw(j-1)],                           points[i, bw(j+1)],
                                                             points[bh(i+1), bw(j-1)], points[bh(i+1), j], points[bh(i+1), bw(j+1)]});
            // update
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    bool was = points[i, j].Life;
                    points[i, j].Update();

                    if (points[i, j].Life != was)
                        changed++;
                }
        }

        public int Zoom { get { return zoom; } set { zoom = Math.Max(1, value); } }
        public int FromWid { get { return fromWid; } set { fromWid = Math.Max(0, Math.Min(width - 1, value)); } }
        public int FromHei { get { return fromHei; } set { fromHei = Math.Max(0, Math.Min(height - 1, value)); } }


        public override string ToString()
        {
            // something more interesting
            string res = String.Format(" Turn: {0}\n Last turn changed: {1}\n\n", turn, changed);

            if (Zoom == 1)
            {
                for (int i = fromHei; i < Math.Min(height, fromHei + Consts.max_height); i++, res += "\n")
                    for (int j = fromWid; j < Math.Min(width, fromWid + Consts.max_width); j++)
                        if (i == Consts.point_hei && j == Consts.point_wid)
                            res += points[i, j].ToStringCrossed();
                        else
                            res += points[i, j].ToString();
                return res;
            }

            int to = Zoom;
            for (int i = fromHei; i < Math.Min(height, fromHei + Consts.max_height * to); i += to, res += "\n")
                for (int j = fromWid; j < Math.Min(width, fromWid + Consts.max_width * to); j += to)
                {
                    short summ = 0; short max = 0; bool found = false;
                    for (int k = 0; k < to; k++) for (int l = 0; l < to; l++, max++)
                        {
                            if (i + k == Consts.point_hei && j + l == Consts.point_wid) found = true;
                            if (points[Math.Min(i + k, height - 1), Math.Min(j + l, width - 1)].Life) summ++;
                        }
                    if (found)
                        res += "X";
                    else
                    {
                        if (summ == 0) res += " ";
                        if (summ > 0 && summ <= max * .25) res += "░";
                        if (summ > max * .25 && summ <= max * .55) res += "▒";
                        if (summ > max * .5 && summ <= max * .75) res += "▓";
                        if (summ > max * .75 && summ <= max * 1) res += "█";
                    }
                }


            res += "";


            return res;
        }

        public bool isDead()
        {
            if (changed_last == changed) not_changed_for++; else not_changed_for = 0;
            return (not_changed_for > 10 || changed == 0);
        }
    }


    struct Point
    {
        short last;
        //public Point[] neight;
        // -1/-1 -1/0 -1/1
        //  0/-1       0/1
        //  1/-1  1/0  1/1
        bool life;
        bool life_become;

        public bool Life { get { return life; } }

        public Point(bool alife)
        {
            this.last = 0;
            this.life = alife;
            this.life_become = this.life;
            //neight = new Point[1];
        }

        public void Calculate(Point[] neight)
        {
            short alife = 0;
            for (int i = 0; i < neight.Length; i++)
                if (neight[i].Life)
                    alife++;
            // if stand steal default
            //life_become = life;
            // if change
            if (!life)
            {
                if (alife == 3)
                    life_become = true;
            }
            else
            {
                if (alife != 2 && alife != 3)
                    life_become = false;
            }
            last = alife;
        }

        public void Update()
        {
            life = life_become;
        }

        public override string ToString()
        {

            if (life)
                return "█";
            else
                return " ";
        }
        public string ToStringCrossed()
        {

            if (life)
                return "▓";
            else
                return "░";
        }

    }
}
//⬙ ⬠ ⬡ ⎔ ⋄ ◊ ⧫ ⬢ ⬣ ▰ ▪ ◼ ▮ ◾ ▗ ▖ ■ ∎ ▃ ▄ ▅ ▆ ▇ █ ▌ ▐ ▍ ▎ ▉ ▊ ▋ ❘ ❙ ❚ ▀ ▘ ▝ ▙ ▚ ▛ ▜ ▟ ▞ //░ ▒ ▓ ▂ ▁ ▬ ▔ ▫ ▯ ▭ ▱ ◽ □ ◻ ▢ ⊞ ⊡ ⊟ ⊠ ▣ ▤ ▥ ▦ ⬚ ▧ ▨ ▩ ⬓ ◧ ⬒ ◨ ◩ ◪ ⬔ ⬕ ❏ ❐ ❑ ❒ ⧈ ◰ ◱ ◳ ◲ ◫ ⧇ ⧅ ⧄ ⍁ ⍂ ⟡ ⧉ ○ ◌ ◍ ◎ ◯ ❍ ◉ ⦾ ⊙ ⦿ ⊜ ⊖ ⊘ ⊚ ⊛ ⊝ ● ⚫ ⦁ ◐ ◑ ◒ ◓ ◔ ◕ ⦶ ⦸ ◵ ◴ ◶ ◷ ⊕ ⊗ ⦇ ⦈ ⦉ ⦊ ❨ ❩ ⸨ ⸩ ◖ ◗ ❪ ❫ ❮ ❯ ❬ ❭ ❰ ❱ ⊏ ⊐ ⊑ ⊒ ◘ ◙ ◚ ◛ ◜ ◝ ◞ ◟ ◠ ◡ ⋒ ⋓ ⋐ ⋑ ╰ ╮ ╭ ╯ ⌒ ╳ ✕ ╱ ╲ ⧸ ⧹ ⌓ ◦ ❖ ✖ ✚ ✜ ⧓ ⧗ ⧑ ⧒ ⧖ _ ⚊ ╴ ╼ ╾ ‐ ⁃ ‑ ‒ - – ⎯ — ― ╶ ╺ ╸ ─ ━ ┄ ┅ ┈ ┉ ╌ ╍ ═ ≣ ≡ ☰ ☱ ☲ ☳ ☴ ☵ ☶ ☷ ╵ ╷ ╹ ╻ │ ▕ ▏ ┃ ┆ ┇ ┊ ╎ ┋ ╿ ╽ ┌ ┍ ┎ ┏ ┐ ┑ ┒ ┓ └ ┕ ┖ ┗ ┘ ┙ ┚ ┛ ├ ┝ ┞ ┟ ┠ ┡ ┢ ┣ ┤ ┥ ┦ ┧ ┨ ┩ ┪ ┫ ┬ ┭ ┮ ┳ ┴ ┵ ┶ ┷ ┸ ┹ ┺ ┻ ┼ ┽ ┾ ┿ ╀ ╁ ╂ ╃ ╄ ╅ ╆ ╇ ╈ ╉ ╊ ╋ ╏ ║ ╔ ╒ ╓ ╕ ╖ ╗ ╚ ╘ ╙ ╛ ╜ ╝ ╞ ╟ ╠ ╡ ╢ ╣ ╤ ╥ ╦ ╧ ╨ ╩ ╪ ╫ ╬ ⌞ ⌟ ⌜ ⌝ ⌊ ⌋ ⌉ ⌈ ⌋ ₯ ἀ ἁ ἂ ἃ ἄ ἅ ἆ ἇ Ἀ Ἁ Ἂ Ἃ Ἄ Ἅ Ἆ Ἇ ἐ ἑ ἒ ἓ ἔ ἕ Ἐ Ἑ Ἒ Ἓ Ἔ Ἕ ἠ ἡ ἢ ἣ ἤ ἥ ἦ ἧ Ἠ Ἡ Ἢ Ἣ Ἤ Ἥ Ἦ Ἧ ἰ ἱ ἲ ἳ ἴ ἵ ἶ ἷ Ἰ Ἱ Ἲ Ἳ Ἴ Ἵ Ἶ Ἷ ὀ ὁ ὂ ὃ ὄ ὅ Ὀ Ὁ Ὂ Ὃ Ὄ Ὅ ὐ ὑ ὒ ὓ ὔ ὕ ὖ ὗ Ὑ Ὓ Ὕ Ὗ ὠ ὡ ὢ ὣ ὤ ὥ ὦ ὧ Ὠ Ὡ Ὢ Ὣ Ὤ Ὥ Ὦ Ὧ ὰ ά ὲ έ ὴ ή ὶ ί ὸ ό ὺ ύ ὼ ώ ᾀ ᾁ ᾂ ᾃ ᾄ ᾅ ᾆ ᾇ ᾈ ᾉ ᾊ ᾋ ᾌ ᾍ ᾎ ᾏ ᾐ ᾑ ᾒ ᾓ ᾔ ᾕ ᾖ ᾗ ᾘ ᾙ ᾚ ᾛ ᾜ ᾝ ᾞ ᾟ ᾠ ᾡ ᾢ ᾣ ᾤ ᾥ ᾦ ᾧ ᾨ ᾩ ᾪ ᾫ ᾬ ᾭ ᾮ ᾯ ᾰ ᾱ ᾲ ᾳ ᾴ ᾶ ᾷ Ᾰ Ᾱ Ὰ Ά ᾼ ᾽ ι ᾿ ῀ ῁ ῂ ῃ ῄ ῆ ῇ Ὲ Έ Ὴ Ή ῌ ῍ ῎ ῏ ῐ ῑ ῒ ΐ ῖ ῗ Ῐ Ῑ Ὶ Ί ῝ ῞ ῟ ῠ ῡ ῢ ΰ ῤ ῥ ῦ ῧ Ῠ Ῡ Ὺ Ύ Ῥ ῭ ΅ ` ῲ ῳ ῴ ῶ ῷ Ὸ Ό Ὼ Ώ ῼ ´ ῾ Ͱ ͱ Ͳ ͳ ʹ ͵ Ͷ ͷ ͺ ͻ ͼ ͽ ; ΄ ΅ Ά · Έ Ή Ί Ό Ύ Ώ ΐ Α Β Γ Δ Ε Ζ Η Θ Ι Κ Λ Μ Ν Ξ Ο Π Ρ Σ Τ Υ Φ Χ Ψ Ω Ϊ Ϋ ά έ ή ί ΰ α β γ δ ε ζ η θ ι κ λ μ ν ξ ο π ρ ς σ τ υ φ χ ψ ω ϊ ϋ ό ύ ώ ϐ ϑ ϒ ϓ ϔ ϕ ϖ ϗ Ϙ ϙ Ϛ ϛ Ϝ ϝ Ϟ ϟ Ϡ ϡ Ϣ ϣ Ϥ ϥ Ϧ ϧ Ϩ ϩ Ϫ ϫ Ϭ ϭ Ϯ ϯ ϰ ϱ ϲ ϳ ϴ ϵ ϶ Ϸ ϸ Ϲ Ϻ ϻ ϼ Ͻ Ͼ Ͽ Ⓐ ⓐ ⒜ A a Ạ ạ Ả ả Ḁ ḁ Â Ã Ǎ ǎ Ấ ấ Ầ ầ Ẩ ẩ Ȃ ȃ Ẫ ẫ Ậ ậ À Á Ắ ắ Ằ ằ Ẳ ẳ Ẵ ẵ Ặ ặ Ā ā Ą ą Ǟ Ȁ ȁ Å Ǻ ǻ Ä ä ǟ Ǡ ǡ â á å ã à ẚ Ȧ ȧ Ⱥ Å ⱥ Æ æ Ǽ Ǣ ǣ Ɐ Ꜳ ꜳ Ꜹ Ꜻ Ɑ ꜹ ꜻ ª ℀ ⅍ ℁ Ⓑ ⓑ ⒝ B b Ḃ ḃ Ḅ ḅ Ḇ ḇ Ɓ Ƀ ƀ ƃ Ƃ Ƅ ƅ ℬ Ⓒ ⓒ ⒞ C c Ḉ ḉ Ć ć Ĉ ĉ Ċ ċ Č č Ç ç Ƈ ƈ Ȼ ȼ ℂ ℃ ℭ Ɔ ℅ ℆ ℄ Ꜿ ꜿ Ⓓ ⓓ ⒟ D d Ḋ ḋ Ḍ ḍ Ḏ ḏ Ḑ ḑ Ḓ ḓ Ď ď Ɗ Ƌ ƌ Ɖ Đ đ ȡ ⅅ ⅆ Ǳ ǲ ǳ Ǆ ǅ ǆ ȸ Ⓔ ⓔ ⒠ E e Ḕ ḕ Ḗ ḗ Ḙ ḙ Ḛ ḛ Ḝ ḝ Ẹ ẹ Ẻ ẻ Ế ế Ẽ ẽ Ề ề Ể ể Ễ ễ Ệ ệ Ē ē Ĕ ĕ Ė ė Ę ę Ě ě È è É é Ê ê Ë ë Ȅ ȅ Ȩ ȩ Ȇ ȇ Ǝ ⱸ Ɇ ℇ ℯ ℮ Ɛ ℰ Ə ǝ ⱻ ɇ Ⓕ ⓕ ⒡ F f Ḟ ḟ Ƒ ƒ ꜰ Ⅎ ⅎ ꟻ ℱ ℻ Ⓖ ⓖ ⒢ G g Ɠ Ḡ ḡ Ĝ ĝ Ğ ğ Ġ ġ Ģ ģ Ǥ ǥ Ǧ ǧ Ǵ ℊ ⅁ ǵ Ⓗ ⓗ ⒣ H h Ḣ ḣ Ḥ ḥ Ḧ ḧ Ḩ ḩ Ḫ ḫ ẖ Ĥ ĥ Ȟ ȟ Ħ ħ Ⱨ ⱨ Ꜧ ℍ Ƕ ℏ ℎ ℋ ℌ ꜧ Ⓘ ⓘ ⒤ I i Ḭ ḭ Ḯ ḯ Ĳ ĳ ì í î ï Ì Í Î Ï Ĩ ĩ Ī ī Ĭ ĭ Į į ı Ɨ ƚ Ỻ Ǐ ǐ ⅈ ⅉ ℹ ℑ ℐ Ⓙ ⓙ ⒥ J j Ĵ ĵ ȷ ⱼ Ɉ ɉ ǰ Ⓚ ⓚ ⒦ K k Ḱ ḱ Ḳ ḳ Ḵ ḵ Ķ ķ Ƙ ƙ Ꝁ ꝁ Ꝃ ꝃ Ꝅ ꝅ Ǩ ǩ Ⱪ ⱪ ĸ Ⓛ ⓛ ⒧ L l Ḷ ḷ Ḹ ḹ Ḻ ḻ Ḽ ḽ Ĺ ĺ Ļ ļ Ľ İ ľ Ŀ ŀ Ł ł Ỉ ỉ Ị ị Ƚ Ⱡ Ꝉ ꝉ ⱡ Ɫ ꞁ ℒ Ǉ ǈ ǉ ⅃ ⅂ ℓ ȉ Ȉ Ȋ ȋ Ⓜ ⓜ ⒨ M m Ḿ ḿ Ṁ ṁ Ṃ ṃ ꟿ ꟽ Ɱ Ʃ Ɯ ℳ Ⓝ ⓝ ⒩ N n Ṅ ṅ Ṇ ṇ Ṉ ṉ Ṋ ṋ Ń ń Ņ ņ Ň ň Ǹ ǹ Ŋ Ɲ ñ ŉ Ñ Ƞ ƞ ŋ Ǌ ǋ ǌ ȵ ℕ №   O o Ṍ ṍ Ṏ ṏ Ṑ ṑ Ṓ ṓ Ȫ ȫ Ȭ ȭ Ȯ ȯ Ȱ ȱ Ǫ ǫ Ǭ ǭ Ọ ọ Ỏ ỏ Ố ố Ồ ồ Ổ ổ Ỗ ỗ Ộ ộ Ớ ớ Ờ ờ Ở ở Ỡ ỡ Ợ ợ Ơ ơ Ō ō Ŏ ŏ Ő ő Ò Ó Ô Õ Ö Ǒ Ȍ ȍ Ȏ ȏ Œ œ Ø Ǿ Ꝋ ǽ ǿ ℴ ⍥ ⍤ Ⓞ ⓞ ⒪ ò ó ô õ ö ǒ ø Ꝏ ꝏ Ⓟ ⓟ ⒫ ℗ P p Ṕ ṕ Ṗ ṗ Ƥ ƥ Ᵽ ℙ Ƿ ꟼ ℘ Ⓠ ⓠ ⒬ Q q Ɋ ɋ ℚ ℺ ȹ Ⓡ ⓡ ⒭ R r Ŕ ŕ Ŗ ŗ Ř ř Ṙ ṙ Ṛ ṛ Ṝ ṝ Ṟ ṟ Ȑ ȑ Ȓ ȓ ɍ Ɍ Ʀ Ɽ ℞ Ꝛ ꝛ ℜ ℛ ℟ ℝ Ⓢ ⓢ ⒮ S s Ṡ ṡ Ṣ ṣ Ṥ ṥ Ṧ ṧ Ṩ ṩ Ś ś Ŝ ŝ Ş ş Š š Ș ș ȿ ꜱ Ƨ ƨ ẞ ß ẛ ẜ ẝ ℠ Ⓣ ⓣ ⒯ T t Ṫ ṫ Ṭ ṭ Ṯ ṯ Ṱ ṱ Ţ ţ Ť ť Ŧ ŧ Ƭ Ʈ ẗ Ț Ⱦ ƫ ƭ ț ⱦ ȶ ℡ ™ Ⓤ ⓤ ⒰ U u Ṳ ṳ Ṵ ṵ Ṷ ṷ Ṹ ṹ Ṻ ṻ Ụ Ủ ủ Ứ Ừ ụ ứ Ử ử ừ ữ Ữ Ự ự Ũ ũ Ū ū Ŭ ŭ Ů ů Ű ű Ǚ ǚ Ǘ ǘ Ǜ ǜ Ų ų Ǔ ǔ Ȕ ȕ Û û Ȗ ȗ Ù ù Ü ü Ư ú Ʉ ư Ʋ Ʊ Ⓥ ⓥ ⒱ V v Ṽ ṽ Ṿ ṿ Ỽ Ʌ ℣ ⱱ ⱴ ⱽ Ⓦ ⓦ ⒲ W w Ẁ ẁ Ẃ ẃ Ẅ ẅ Ẇ ẇ Ẉ ẉ Ŵ ŵ ẘ Ⱳ ⱳ Ⓧ ⓧ ⒳ X x Ẋ ẋ Ẍ ẍ ℵ × Ⓨ ⓨ ⒴ y Y Ẏ ẏ Ỿ ỿ ẙ Ỳ ỳ Ỵ ỵ Ỷ ỷ Ỹ ỹ Ŷ ŷ Ƴ ƴ Ÿ ÿ Ý ý Ɏ ɏ Ȳ Ɣ ⅄ ȳ ℽ Ⓩ ⓩ ⒵ Z z Ẑ ẑ Ẓ ẓ Ẕ ẕ Ź ź Ż ż Ž ž Ȥ ȥ Ⱬ ⱬ Ƶ ƶ ɀ ℨ ℤ ⟀ ⟁ ⟂ ⟃ ⟄ ⟇ ⟈ ⟉ ⟊ ⟐ ⟑ ⟒ ⟓ ⟔ ⟕ ⟖ ⟗ ⟘ ⟙ ⟚ ⟛ ⟜ ⟝ ⟞ ⟟ ⟠ ⟡ ⟢ ⟣ ⟤ ⟥ ⟦ ⟧ ⟨ ⟩ ⟪ ⟫ ⦀ ⦁ ⦂ ⦃ ⦄ ⦅ ⦆ ⦇ ⦈ ⦉ ⦊ ⦋ ⦌ ⦍ ⦎ ⦏ ⦐ ⦑ ⦒ ⦓ ⦔ ⦕ ⦖ ⦗ ⦘ ⦙ ⦚ ⦛ ⦜ ⦝ ⦞ ⦟ ⦠ ⦡ ⦢ ⦣ ⦤ ⦥ ⦦ ⦧ ⦨ ⦩ ⦪ ⦫ ⦬ ⦭ ⦮ ⦯ ⦰ ⦱ ⦲ ⦳ ⦴ ⦵ ⦶ ⦷ ⦸ ⦹ ⦺ ⦻ ⦼ ⦽ ⦾ ⦿ ⧀ ⧁ ⧂ ⧃ ⧄ ⧅ ⧆ ⧇ ⧈ ⧉ ⧊ ⧋ ⧌ ⧍ ⧎ ⧏ ⧐ ⧑ ⧒ ⧓ ⧔ ⧕ ⧖ ⧗ ⧘ ⧙ ⧚ ⧛ ⧜ ⧝ ⧞ ⧟ ⧡ ⧢ ⧣ ⧤ ⧥ ⧦ ⧧ ⧨ ⧩ ⧪ ⧫ ⧬ ⧭ ⧮ ⧯ ⧰ ⧱ ⧲ ⧳ ⧴ ⧵ ⧶ ⧷ ⧸ ⧹ ⧺ ⧻ ⧼ ⧽ ⧾ ⧿ ∀ ∁ ∂ ∃ ∄ ∅ ∆ ∇ ∈ ∉ ∊ ∋ ∌ ∍ ∎ ∏ ∐ ∑ − ∓ ∔ ∕ ∖ ∗ ∘ ∙ √ ∛ ∜ ∝ ∞ ∟ ∠ ∡ ∢ ∣ ∤ ∥ ∦ ∧ ∨ ∩ ∪ ∫ ∬ ∭ ∮ ∯ ∰ ∱ ∲ ∳ ∴ ∵ ∶ ∷ ∸ ∹ ∺ ∻ ∼ ∽ ∾ ∿ ≀ ≁ ≂ ≃ ≄ ≅ ≆ ≇ ≈ ≉ ≊ ≋ ≌ ≍ ≎ ≏ ≐ ≑ ≒ ≓ ≔ ≕ ≖ ≗ ≘ ≙ ≚ ≛ ≜ ≝ ≞ ≟ ≠ ≡ ≢ ≣ ≤ ≥ ≦ ≧ ≨ ≩ ≪ ≫ ≬ ≭ ≮ ≯ ≰ ≱ ≲ ≳ ≴ ≵ ≶ ≷ ≸ ≹ ≺ ≻ ≼ ≽ ≾ ≿ ⊀ ⊁ ⊂ ⊃ ⊄ ⊅ ⊆ ⊇ ⊈ ⊉ ⊊ ⊋ ⊌ ⊍ ⊎ ⊏ ⊐ ⊑ ⊒ ⊓ ⊔ ⊕ ⊖ ⊗ ⊘ ⊙ ⊚ ⊛ ⊜ ⊝ ⊞ ⊟ ⊠ ⊡ ⊢ ⊣ ⊤ ⊥ ⊦ ⊧ ⊨ ⊩ ⊪ ⊫ ⊬ ⊭ ⊮ ⊯ ⊰ ⊱ ⊲ ⊳ ⊴ ⊵ ⊶ ⊷ ⊸ ⊹ ⊺ ⊻ ⊼ ⊽ ⊾ ⊿ ⋀ ⋁ ⋂ ⋃ ⋄ ⋅ ⋆ ⋇ ⋈ ⋉ ⋊ ⋋ ⋌ ⋍ ⋎ ⋏ ⋐ ⋑ ⋒ ⋓ ⋔ ⋕ ⋖ ⋗ ⋘ ⋙ ⋚ ⋛ ⋜ ⋝ ⋞ ⋟ ⋠ ⋡ ⋢ ⋣ ⋤ ⋥ ⋦ ⋧ ⋨ ⋩ ⋪ ⋫ ⋬ ⋭ ⋮ ⋯ ⋰ ⋱ ⋲ ⋳ ⋴ ⋵ ⋶ ⋷ ⋸ ⋹ ⋺ ⋻ ⋼ ⋽ ⋾ ⋿ ✕ ✖ ✚ ◀ ▶ ❝ ❞ ★ ☆ ☼ ☂ ☺ ☹ ✄ ✈ ✌ ✎ ♪ ♫ ☀ ☁ ☔ ⚡ ❆ ☽ ☾ ✆ ✔ ☯ ☮ ☠ ☬ ✄ ✏ ♰ ✡ ✰ ✺ ♕ ♛ ♚ ♬ ⓐ ⓑ ⓒ ⓓ ↺ ↻ ⇖ ⇗ ⇘ ⇙ ⟵ ⟷ ⟶ ⤴ ⤵ ⤶ ⤷ ➫ ➬ € ₤ ＄ ₩ ₪ ⟁ ⟐ ◆ ⎔ ░ ▢ ⊡ ▩ ⟡ ◎ ◵ ⊗ ❖ Ω β Φ Σ Ξ ⟁ ⦻ ⧉ ⧭ ⧴ ∞ ≌ ⊕ ⋍ ⋰ ⋱ ✖ ⓵ ⓶ ⓷ ⓸ ⓹ ⓺ ⓻ ⓼ ⓽ ⓾ ᴕ ⸨ ⸩ ❪ ❫ ⓵ ⓶ ⓷ ⓸ ⓹ ⓺ ⓻ ⓼ ⓽ ⓾ ⒈ ⒉ ⒊ ⒋ ⒌ ⒍ ⒎ ⒏ ⒐ ⒑ ⒒ ⒓ ⒔ ⒕ ⒖ ⒗ ⒘ ⒙ ⒚ ⒛ ⓪ ① ② ③ ④ ⑤ ⑥ ⑦ ⑧ ⑨ ⑩ ➀ ➁ ➂ ➃ ➄ ➅ ➆ ➇ ➈ ➉ ⑪ ⑫ ⑬ ⑭ ⑮ ⑯ ⑰ ⑱ ⑲ ⑳ ⓿ ❶ ❷ ❸ ❹ ❺ ❻ ❼ ❽ ❾ ❿ ➊ ➋ ➌ ➍ ➎ ➏ ➐ ➑ ➒ ➓ ⓫ ⓬ ⓭ ⓮ ⓯ ⓰ ⓱ ⓲ ⓳ ⓴ ⑴ ⑵ ⑶ ⑷ ⑸ ⑹ ⑺ ⑻ ⑼ ⑽ ⑾ ⑿ ⒀ ⒁ ⒂ ⒃ ⒄ ⒅ ⒆ ⒇ ᶅ ᶛ ᶜ ᶝ ᶞ ᶟ ᶠ ᶡ ᶢ ᶣ ᶤ ᶥ ᶦ ᶧ ᶨ ᶩ ᶪ ᶫ ᶬ ᶭ ᶮ ᶯ ᶰ ᶱ ᶲ ᶳ ᶴ ᶵ ᶶ ᶷ ᶹ ᶺ ᶻ ᶼ ᶽ ᶾ ᶿ ᴀ ᴁ ᴂ ᴃ ᴄ ᴅ ᴆ ᴇ ᴈ ᴉ ᴊ ᴋ ᴌ ᴍ ᴎ ᴏ ᴐ ᴑ ᴒ ᴓ ᴔ ᴕ ᴖ ᴗ ᴘ ᴙ ᴚ ᴛ ᴜ ᴝ ᴞ ᴟ ᴠ ᴡ ᴢ ᴣ ᴤ ᴥ ᴦ ᴧ ᴨ ᴩ ᴪ ᴫ ᴬ ᴭ ᴮ ᴯ ᴰ ᴱ ᴲ ᴳ ᴴ ᴵ ᴶ ᴷ ᴸ ᴹ ᴺ ᴻ ᴼ ᴽ ᴾ ᴿ ᵀ ᵁ ᵂ ᵃ ᵄ ᵅ ᵆ ᵇ ᵈ ᵉ ᵊ ᵋ ᵌ ᵍ ᵎ ᵏ ᵐ ᵑ ᵒ ᵓ ᵔ ᵕ ᵖ ᵗ ᵘ ᵙ ᵚ ᵛ ᵜ ᵝ ᵞ ᵟ ᵠ ᵡ ᵢ ᵣ ᵤ ᵥ ᵦ ᵧ ᵨ ᵩ ᵪ ᵫ ᵬ ᵭ ᵮ ᵱ ᵲ ᵳ ᵵ ᵷ ᵸ ᵺ ᵻ ᷋ ᷌ ᷍ ᷎ ᷏ ᷓ ᷔ ᷕ ᷖ ᷗ ᷘ ᷙ ᷛ ᷜ ᷝ ᷞ ᷟ ᷠ ᷡ ᷢ ᷣ ᷤ ᷥ ᷦ ‘ ’ ‛ ‚ “ ” „ ‟ « » ‹ › Ꞌ " ❛ ❜ ❝ ❞ < > @ ‧ ¨ ․ ꞉ : ⁚ ⁝ ⁞ ‥ … ⁖ ⸪ ⸬ ⸫ ⸭ ⁛ ⁘ ⁙ ⁏ ; ⦂ ⁃ ‐ ‑ ‒ - – ⎯ — ― _ ⁓ ⸛ ⸞ ⸟ ⸯ ¬ / \ ⁄ \ ⁄ | ⎜ ¦ ‖ ‗ † ‡ · • ⸰ ° ‣ ⁒ % ‰ ‱ & ⅋ § ÷ + ± = ꞊ ′ ″ ‴ ⁗ ‵ ‶ ‷ ‸ * ⁑ ⁎ ⁕ ※ ⁜ ⁂ ! ‼ ¡ ? ¿ ⸮ ⁇ ⁉ ⁈ ‽ ⸘ ¼ ½ ¾ ² ³ © ® ™ ℠ ℻ ℅ ℁ ⅍ ℄ ¶ ⁋ ❡ ⁌ ⁍ ⸖ ⸗ ⸚ ⸓ ( ) [ ] { } ⸨ ⸩ ❨ ❩ ❪ ❫ ⸦ ⸧ ❬ ❭ ❮ ❯ ❰ ❱ ❴ ❵ ❲ ❳ ⦗ ⦘ ⁅ ⁆ 〈 〉 ⏜ ⏝ ⏞ ⏟ ⸡ ⸠ ⸢ ⸣ ⸤ ⸥ ⎡ ⎤ ⎣ ⎦ ⎨ ⎬ ⌠ ⌡ ⎛ ⎠ ⎝ ⎞ ⁀ ⁔ ‿ ⁐ ‾ ⎟ ⎢ ⎥ ⎪ ꞁ ⎮ ⎧ ⎫ ⎩ ⎭ ⎰ ⎱ ✈ ☀ ☼ ☁ ☂ ☔ ⚡ ❄ ❅ ❆ ☃ ☉ ☄ ★ ☆ ☽ ☾ ⌛ ⌚ ☇ ☈ ⌂ ⌁ ✆ ☎ ☏ ☑ ✓ ✔ ⎷ ⍻ ✖ ✗ ✘ ☒ ✕ ☓ ☕ ♿ ✌ ☚ ☛ ☜ ☝ ☞ ☟ ☹ ☺ ☻ ☯ ⚘ ☮ ✝ ⚰ ⚱ ⚠ ☠ ☢ ⚔ ⚓ ⎈ ⚒ ☡ ❂ ⚕ ⚖ ⚗ ✇ ☣ ⚙ ☤ ⚚ ⚜ ☥ ☦ ☧ ☨ ☩ † ☪ ☫ ☬ ☭ ✁ ✂ ✃ ✄ ✍ ✎ ✏ ✐  ✑ ✒ ✉ ✙ ✚ ✜ ✛ ♰ ♱ ✞ ✟ ✠ ✡ ☸ ✢ ✣ ✤ ✥ ✦ ✧ ✩ ✪ ✫ ✬ ✭ ✮ ✯ ✰ ✲ ✱ ✳ ✴ ✵ ✶ ✷ ✸ ✹ ✺ ✻ ✼ ✽ ✾ ❀ ✿ ❁ ❃ ❇ ❈ ❉ ❊ ❋ ⁕ ☘ ❦ ❧ ☙ ❢ ❣ ♀ ♂ ⚤ ⚦ ⚧ ⚨ ⚩ ☿ ♁ ⚯ ♔ ♕ ♖ ♗ ♘ ♙ ♚ ♛ ♜ ♝ ♞ ♟ ☖ ☗ ♠ ♣ ♦ ♥ ❤ ❥ ♡ ♢ ♤ ♧ ⚀ ⚁ ⚂ ⚃ ⚄ ⚅ ⚇ ⚆ ⚈ ⚉ ♨ ♩ ♪ ♫ ♬ ♭ ♮ ♯ ⏏ ⎗ ⎘ ⎙ ⎚ ⎇ ⌘ ⌦ ⌫ ⌧ ♲ ♳ ♴ ♵ ♶ ♷ ♸ ♹ ♻ ♼ ♽ ⁌ ⁍ ⎌ ⌇ ⍝ ⍟ ⍣ ⍤ ⍥ ⍨ ⍩ ⎋ ♃ ♄ ♅ ♆ ♇ ♈ ♉ ♊ ♋ ♌ ♍ ♎ ♏ ♐ ♑ ♒ ♓ ⏚ ⏛ |   |   |   |   |   |   |   |   |   | ​ |
