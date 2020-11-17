using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MySql.Data.MySqlClient;

namespace Aula13_GravandoDadosMySQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            lstContatos.GridLines = true;
            lstContatos.View = View.Details;

            lstContatos.Columns.Add("Id", 30, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Email", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Telefone", 150, HorizontalAlignment.Left);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Conectar no mysql (servidor) utilizando o usuário e senha.
            // Gravar os registros na tabela de contato
            // Avisar o usuário que deu tudo certo ou tudo errado

            try
            {
                // Abrindo a conexão com o MySQL.
                string data_source = "datasource=localhost;username=root;password=;database=db_agenda";
                MySqlConnection Conexao = new MySqlConnection(data_source);
                Conexao.Open();


                // Gravando dados no MySQL
                //string sql = "INSERT INTO contato (nome, email, telefone) VALUES ('Tiago', 'tiago@fatec', '169225255') ";
                //string sql = "INSERT INTO contato (nome, email, telefone) VALUES ('" + txtNome.Text + "', 'tiago@fatec', '169225255') ";

                string sql = String.Format("INSERT INTO contato (nome, email, telefone) " +
                    "                       VALUES ('{0}', '{1}', '{2}')", 
                                            txtNome.Text, txtEmail.Text, txtTelefone.Text);
                MySqlCommand cmd = new MySqlCommand(sql, Conexao);
                cmd.ExecuteNonQuery();

                // Mostrando para o usuário que deu certo.
                MessageBox.Show("Deu certo, conectou e gravou.");


            } catch(Exception ex)
            {
                MessageBox.Show("Deu errado, erro: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Conectar no MySQL
            // Enviar comando de listagem de registros
            // Mostrados dados obtidos para o usuário

            try
            {
                // Abrindo a conexão com o MySQL.
                string data_source = "datasource=localhost;username=root;password=;database=db_agenda";
                MySqlConnection Conexao = new MySqlConnection(data_source);
                Conexao.Open();


                // Executando o comando de "leitura" dos dados.
                string sql = "SELECT id, nome, email, telefone FROM contato";
                MySqlCommand cmd = new MySqlCommand(sql, Conexao);


                // Estruturar os dados recebidos do mysql e mostrar ao usuário
                // reader = leitor
                MySqlDataReader reader = cmd.ExecuteReader();

                lstContatos.Items.Clear();

                while(reader.Read())
                {
                    string[] linha = { reader.GetString(0), //pega a id
                                       reader.GetString(1), // pega o nome
                                       reader.GetString(2), // pega o email
                                       reader.GetString(3), // pega o telefone
                                     };

                    ListViewItem estrutura_da_linha = new ListViewItem(linha);

                    lstContatos.Items.Add(estrutura_da_linha);
                }


            } catch(Exception ex)
            {
                MessageBox.Show("Deu errado, erro: " + ex.Message);
            }
        }
    }
}
