using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication8
{
    public partial class Form1 : Form
    {

        Animal dog;
        Animal cat;

        Animal dogRef;
        WeakReference catRef;



        public Form1()
        {
            InitializeComponent();

            //Animal라는 클래스를 만들어 각각 dog, cat의 이름을 만들어 주었습니다.
            dog = new Animal("Mango");
            cat = new Animal("KiKi");

            //각각 참조를 거는데 dog은 일반적으로 사용하는 강한참조
            //cat은 WeakReference를 이용한 약한 참조를 걸어보도록 하겠습니다.
            dogRef = dog;
            catRef = new WeakReference(cat);

            //각각 라벨에 출력해보겠습니다.
            labelControl1.Text = dogRef == null ? "null" : dogRef.name;
            labelControl2.Text = catRef.Target == null ? "null" : (catRef.Target as Animal).name;

            //당연한 이야기겠지만
            //------------  label1 = Mango
            //------------  label2 = KiKi
            //가 출력됩니다.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class Animal
        {
            public string name = "";
            public Animal(string i_name)
            {
                this.name = i_name;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //버튼 클릭시 dog과 cat에 null을 넣어 초기화 시켜줍니다.
            dog = null;
            cat = null;

            //가바지 컬렉터를 강제로 작동시켜보겠습니다.
            System.GC.Collect(0, GCCollectionMode.Forced);
            System.GC.WaitForFullGCComplete();

            //다시한번 참조한 값을 각각 라벨에 출력해보겠습니다.
            labelControl1.Text = dogRef == null ? "null" : dogRef.name;
            labelControl2.Text = catRef.Target == null ? "null" : (catRef.Target as Animal).name;


            // dogRef과 catRef 모두 참조 객체가 null로 변경이 되었으니 null이 출력된다고 생각했습니다.
            //하지만 결과는
            //------------  label1 = Mango
            //------------  label2 = KiKi
            //과 같이 출력됩니다.
            //강한참조는 참조 객체를 초기화 시켜도 Data를 붙잡고 있어서 메모리 회수가 안되는 것입니다.
        }
    }
}
