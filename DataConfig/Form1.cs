using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataConfig
{
    public partial class Form1 : Form
    {
        private static ISessionFactory sessionFactory;
        private string connectionString = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            connectionString = String.Format("Server={0};Database={1};User Id={2};Password={3}", textBoxServer.Text, textBoxDataBase.Text, textBoxUserId.Text, textBoxPassword.Text);

            try
            {
                InitSessionFactory(connectionString);
                textResultado.Text = textResultado.Text + "\r\nEsquema Exportado con éxito...";
                crearConfig(connectionString);
                textResultado.Text = textResultado.Text + "\r\nArchivo configuracion escrito con exito...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error: {0}\n{1}", ex.Message, ex.InnerException.ToString()));
            }
            finally
            {
            }
        }

        static void InitSessionFactory(string cnn)
        {
            // SQL Server
            var cfg = FluentNHibernate.Cfg.Db.MsSqlConfiguration
                .MsSql2008
                .ConnectionString(c => c.Is(cnn));

            sessionFactory = Fluently
                 .Configure()
                 .Database(cfg)
                 .Mappings(m =>
                 {
                     m.FluentMappings.AddFromAssemblyOf<Data.ControlCenter.Model.AccountUser>().ExportTo(@".\");                         
                     //m.FluentMappings.AddFromAssemblyOf<Data.ControlCenter.Model.JWToken>().ExportTo(@".\");
                 })
                 .ExposeConfiguration(BuildSchema)
                 .BuildSessionFactory();
        }

        public void CreationDB(string cnn)
        {
            FluentConfiguration config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(cnn))
                .Mappings(
                    m =>
                    {
                        m.FluentMappings.AddFromAssemblyOf<Data.ControlCenter.Model.AccountUser>().ExportTo(@".\");
                      //  m.FluentMappings.AddFromAssemblyOf<Data.ControlCenter.Model.JWToken>().ExportTo(@".\");
                    }
                );

            config.ExposeConfiguration(
                      c => new SchemaExport(c).Execute(true, true, false))
                 .BuildConfiguration();
        }

        static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info in)        
            // and exports a database schema from it        
            //new SchemaExport(config)
            //    .Create(true, true);
            new SchemaUpdate(config).Execute(false, true);
        }

        private void buttonData_Click(object sender, EventArgs e)
        {
            //Data Source=192.168.16.3\DISDELSQL;Initial Catalog=MyBussines;User ID=sa; Password=P@ssw0rd
            string cn = String.Format("Data Source={0};Initial Catalog={1};User={2};Password={3};", textBoxServer.Text, textBoxDataBase.Text, textBoxUserId.Text, textBoxPassword.Text);
            try
            {
                ejecutarScript(cn);
                textResultado.Text = textResultado.Text + "\r\nScript ejecutado con exito...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error: {0}\n{1}", ex.Message, ex.InnerException.ToString()));
            }
            finally
            {
            }
        }

        static void crearConfig(string s)
        {
            string path = @"C:\MBConfig";
            try
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Process failed {0}.", e.ToString()));
            }

            System.IO.File.WriteAllText(@"C:\MBConfig\cfg.txt", s);
        }

        static void ejecutarScript(string cnn)
        {
            FileInfo file = new FileInfo(@"..\script.sql");


            string script = file.OpenText().ReadToEnd();


            using (SqlConnection conn = new SqlConnection(cnn))
            {
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(script, conn);
                sqlCmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // Form1
        //    // 
        //    this.ClientSize = new System.Drawing.Size(284, 261);
        //    this.Name = "Form1";
        //    this.Load += new System.EventHandler(this.Form1_Load);
        //    this.ResumeLayout(false);
        //}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // Form1
        //    // 
        //    this.ClientSize = new System.Drawing.Size(284, 261);
        //    this.Name = "Form1";
        //    this.Load += new System.EventHandler(this.Form1_Load_1);
        //    this.ResumeLayout(false);
        //}
            
      
    }
}
