using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsEF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 两个savechange之间throwEx,table_A保存，table_B失败
            using (PNMEntities entity = new PNMEntities())
            {
                var a = entity.table_A.Where(x => x.id == 101).FirstOrDefault();

                a.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                entity.SaveChanges();

                throw new Exception();

                var b = entity.table_B.Where(x => x.id == 202).FirstOrDefault();

                b.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                entity.SaveChanges();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 一个savechange之前throwEx,table_A、table_B失败
            using (PNMEntities entity = new PNMEntities())
            {
                var a = entity.table_A.Where(x => x.id == 101).FirstOrDefault();

                a.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                throw new Exception();

                var b = entity.table_B.Where(x => x.id == 202).FirstOrDefault();

                b.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                entity.SaveChanges();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 一个savechange之后throwEx,table_A、table_B成功
            using (PNMEntities entity = new PNMEntities())
            {
                var a = entity.table_A.Where(x => x.id == 101).FirstOrDefault();

                a.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var b = entity.table_B.Where(x => x.id == 202).FirstOrDefault();

                b.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                entity.SaveChanges();

                throw new Exception();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //  sleep 10秒，sleep时，数据仍能查询，是旧值
            using (PNMEntities entity = new PNMEntities())
            {
                var a = entity.table_A.Where(x => x.id == 101).FirstOrDefault();

                a.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                System.Threading.Thread.Sleep(10000);

                var b = entity.table_B.Where(x => x.id == 202).FirstOrDefault();

                b.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                entity.SaveChanges();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 加入事务 sleep 10秒，sleep时，如果有Savechange，则会锁表，无法查询，如果无，则数据仍能查询，是旧值
            // 若无SaveChanges 或者Commit 都不能保存
            using (PNMEntities entity = new PNMEntities())
            {
                using (DbContextTransaction tran = entity.Database.BeginTransaction())
                {

                    var a = entity.table_A.Where(x => x.id == 101).FirstOrDefault();

                    a.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    entity.SaveChanges();

                    System.Threading.Thread.Sleep(10000);

                    var b = entity.table_B.Where(x => x.id == 202).FirstOrDefault();

                    b.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    entity.SaveChanges();

                    tran.Commit();


                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 加入事务 rollback 后，值不会保存至表，
            // 但再进行SaveChange()后，会直接提交，但Commit时出错
            // 是否表明，entity.SaveChange()时，会检查有没有事务，如果无，则会自己生成
            using (PNMEntities entity = new PNMEntities())
            {
                using (DbContextTransaction tran = entity.Database.BeginTransaction())
                {

                    var a = entity.table_A.Where(x => x.id == 101).FirstOrDefault();

                    a.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 

                    var b = entity.table_B.Where(x => x.id == 202).FirstOrDefault();

                    b.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    entity.SaveChanges();

                    tran.Rollback();

                    //a.note = "Testa";

                    //b.note = "Testb";

                    //entity.SaveChanges();

                    //tran.Commit();


                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // 效果一致
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                using (PNMEntities entity = new PNMEntities())
                {
                    var a = entity.table_A.Where(x => x.id == 101).FirstOrDefault();

                    a.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 

                    var b = entity.table_B.Where(x => x.id == 202).FirstOrDefault();

                    b.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    entity.SaveChanges(); 

                    System.Threading.Thread.Sleep(10000);

                    scope.Complete();

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 使用TransactionScope也可以达到目的，而且可以跨不同的entity进行操作
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                using (PNMEntities entity = new PNMEntities())
                {
                    var a = entity.table_A.Where(x => x.id == 101).FirstOrDefault();

                    a.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 

                    entity.SaveChanges(); 

                }
                
                System.Threading.Thread.Sleep(10000);

                throw new Exception();

                using (PNMEntities1 entity1 = new PNMEntities1())
                {
                    var b = entity1.table_B.Where(x => x.id == 202).FirstOrDefault();

                    b.note = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    entity1.SaveChanges(); 
                }

                scope.Complete();
            }
        }


    }
}
