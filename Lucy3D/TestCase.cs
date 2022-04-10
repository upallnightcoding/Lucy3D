
using Lucy3DLib;

namespace Lucy3D
{
    class TestCase
    {
        public static string Source01 = @"
            integer x = 10, y = 55;     
            print x, y;                 
        ";

        public static SourceCode Code()
        {
            SourceCode source = new SourceCode(Source01);

            return (source);
        }

        public static SourceCode ArrayTest01()
        {
            SourceCode source = new SourceCode();

            source.Add("program test {");
            source.Add("    integer x = 10, y = 3;");
            source.Add("    integer b[x+1, y+3*2];");
            source.Add("    b[2,3] = 50;");
            source.Add("    b[0,1] = 60;");
            source.Add("    b[2,2] = 70;");
            source.Add("    print `b[2,3]:  `, b[1+1, 1+1+1] ;");
            source.Add("    print `b[0,1]:  `, b[0,0+1] ;");
            source.Add("    print `b[2,2]:  `, b[2,1+1] ;");
            source.Add("    print 7 - 5 + 1;");
            source.Add("}");

            return (source);
        }

        public static SourceCode SysFunc01()
        {
            SourceCode source = new SourceCode();

            source.Add("program test {");
            source.Add("    print `123: `, imax(123, 23, 3);");
            source.Add("    print `99: `, imax(45, 99, 2);");
            source.Add("    print `987: `, imax(0, 987, 45);");
            source.Add("    print `3: `, imin(123, 23, 3);");
            source.Add("    print `2: `, imin(45, 99, 2);");
            source.Add("    print `0: `, imin(0, 987, 45);");
            source.Add("    print `isum: `, isum(123, 23, 3);");
            source.Add("    print `2: `, isum(1.9, 1.9);");
            source.Add("    print `isum: `, isum(0, 987, 45);");
            source.Add("    print `10: `, isum(1, 2, 3, 4);");
            source.Add("    print `10: `, isum(isum(1, 2), isum(3,isum(1, 2, 1)));");
            source.Add("    print `false: `, false();");
            source.Add("    print `true: `, true();");
            source.Add("    print `true: `, not(false());");
            source.Add("    print `false: `, not(true());");
            source.Add("}");

            return (source);
        }

        public static SourceCode Errors()
        {
            SourceCode source = new SourceCode();

            source.Add("program test {");

            source.Add("    print 1 + 2 % 3;");
           
            source.Add("}");

            return (source);
        }

        public static SourceCode ArrayTest02()
        {
            SourceCode source = new SourceCode();

            source.Add("program test {");

            source.Add("    integer x = 10, y = 3;");
            source.Add("    integer b[x, y], a[20];");
            source.Add("    print a[10];");
            source.Add("    a[0] = 10;");
            source.Add("    a[2] = 20;");
            source.Add("    a[3] = 30;");
            source.Add("    b[3,0] = 50;");
            source.Add("    b[0,1] = 60;");
            source.Add("    b[2,2] = 70;");
            source.Add("    print \"a[0]  \", a[0] ;");
            source.Add("    print \"a[2]  \", a[2] ;");
            source.Add("    print \"a[3]  \", a[3] ;");
            source.Add("    print \"b[3,0]  \", b[3,0] ;");
            source.Add("    print \"b[0,1]  \", b[0,1] ;");
            source.Add("    print \"b[2,2]  \", b[2,2] ;");
            //source.Add("    integer a = (1 + 2.2), b[x, y], c[3+1,4*2];");
            //source.Add("    print \"Value a1:  \", a ;");
            //source.Add("    a = 33 + 11;");
            //source.Add("    print \"Value a2:  \", a ;");
            source.Add("}");

            return (source);
        }

        public static SourceCode TestLogical01()
        {
            SourceCode source = new SourceCode();

            source.Add("program test {");
            source.Add("    integer a = (1 + 2.2), b = 4.67 / 2.0, c = 3.01;");
            source.Add("    float x = (1 + 2.2), y = 4.67 / 2.0, z = 3.01;");
            source.Add("    print 1 + 9 == 2 * 5, ' ', a == a, ' ', a == 1;");
            source.Add("    print 1 < 10, ' ', a < a, ' ', 10 < 1;");
            source.Add("    print 1 <= 10, ' ', a <= a, ' ', 10 <= 1;");
            source.Add("    print 1 > 10, ' ', a > a, ' ', 10 > 1;");
            source.Add("    print 1 >= 10, ' ', a >= a, ' ', 10 >= 1;");
            source.Add("    print 1 != 10, ' ', a != a, ' ', 10 != 1;");
            source.Add("    print 10 - 4, ' ', 4 - 10, ' ', 16.3 - 3, ' ', 16 - 5.5;");
            source.Add("    print a, ` This is a back tick test: `, b, ``, c ;");
            source.Add("    print `x: `, x, ` y: `, y, ` z: `, z ;");
            source.Add("}");

            return (source);
        }

        /*public static Scene Case03()
        {
            xxSprite s1 = new xxSprite();
            s1.Add(new ActionMove(0.00005f, 0.0f, 0.0f));
            s1.Add(new ActionTurn(0.0f, 0.0f, 0.01f));

            xxSprite s2 = new xxSprite();
            s2.Add(new ActionMove(-0.00005f, 0.0f, 0.0f));

            Scene scene = new Scene();
            scene.Add(s1);
            scene.Add(s2);

            return (scene);
        }

        public static Scene Case04()
        {
            State state = new State("Move");
            state.Add(new ActionMove(-0.001f, 0.0f, 0.0f));

            FSM fsm = new FSM();
            fsm.Add(state);

            xxSprite sprite = new xxSprite();
            sprite.Set(fsm);

            Scene scene = new Scene();
            scene.Add(sprite);

            return (scene);
        }*/

        public static SourceCode Case05()
        {
            SourceCode source = new SourceCode();

            source.Add("program test {");
            source.Add("    print \"abc\" == \"abc\";");
            source.Add("    print \"xyz\" == \"abc\";");
            source.Add("    print 1 == 1, \" \",3.2 == 5.0;");
            source.Add("    print '9';");
            source.Add("    print '3', \" \", '1' == '1', \" \",3.2 == 5.0;");
            source.Add("}");

            return (source);
        }
    }
}
