using Oracle.ManagedDataAccess.Client;
using OracleListener.Data;
using OracleListener.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleListener
{
    public partial class FormHesapPlani : Form
    {
        public FormHesapPlani()
        {
            InitializeComponent();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GetPlans()
        {
            try
            {
                Task.Run(() =>
                {
                    using (OracleProvider db = new OracleProvider())
                    {
                        //db.ExecuteScalar("SELECT COUNT(*) TB_COUNT FROM ALL_TABLES WHERE TABLE_NAME = 'ZFIND_SAGE_HESAPPLANI'");

                        var plans = db.Select<SAGE_HESAPPLANI>("SELECT * FROM \"UYUMSOFT\".\"ZFIND_SAGE_HESAPPLANI\"");
                        if (plans != null && plans.Count > 0)
                        {
                            listView1.Invoke(new Action(() =>
                            {
                                listView1.BeginUpdate();
                                listView1.Items.Clear();
                                for (int i = 0; i < plans.Count; i++)
                                {
                                    ListViewItem item = new ListViewItem();
                                    item.Text = plans[i].HESAPPLANI_ID.ToString();
                                    item.Tag = plans[i];
                                    item.SubItems.Add(plans[i].ACC_CODE);
                                    item.SubItems.Add(plans[i].HESAPPLANI_CODE);
                                    listView1.Items.Add(item);
                                }
                                listView1.EndUpdate();
                                Application.DoEvents();
                            }));
                        }
                    }

                });
            }
            catch (Exception exception)
            {
                Logger.E(exception);
            }
        }

        private void FormHesapPlani_Load(object sender, EventArgs e)
        {
            GetPlans();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                SAGE_HESAPPLANI plan = listView1.Items[listView1.SelectedIndices[0]].Tag as SAGE_HESAPPLANI;
                textId.Tag = plan;
                textId.Text = plan.HESAPPLANI_ID.ToString();
                textMuhasebe.Text = plan.HESAPPLANI_CODE;
                textacc.Text = plan.ACC_CODE;
                btnkaydet.Text = "Güncelle";
                Application.DoEvents();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SAGE_HESAPPLANI plan = textId.Tag as SAGE_HESAPPLANI;
            if (plan != null && MessageBox.Show("Hesap plan tanımı silinsin mi?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (OracleProvider db = new OracleProvider())
                {
                    if (!db.Execute($"DELETE FROM \"UYUMSOFT\".\"ZFIND_SAGE_HESAPPLANI\" WHERE \"HESAPPLANI_ID\" = '{plan.HESAPPLANI_ID}' "))
                    {
                        ShowError($"Kayıt silinemedi! {db.Message}");
                    }
                    else
                    {
                        textId.Tag = null;
                        textId.Text = "";
                        textMuhasebe.Text = "";
                        textacc.Text = "";
                        Application.DoEvents();
                        GetPlans();
                    }
                }
            }
        }

        private void btnyeni_Click(object sender, EventArgs e)
        {
            textId.Tag = null;
            textId.Text = "";
            textacc.Text = "";
            textMuhasebe.Text = "";
            btnkaydet.Text = "Kaydet";
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = null;
                OracleParameter[] parameters = null;
                SAGE_HESAPPLANI plan = textId.Tag as SAGE_HESAPPLANI;
                if (plan == null)
                {
                    parameters = new OracleParameter[] {
                        new OracleParameter("@PLAN",textacc.Text),
                        new OracleParameter("@HESAP",textMuhasebe.Text)
                    };
                    sql = @"INSERT INTO ""UYUMSOFT"".""ZFIND_SAGE_HESAPPLANI"" (""HESAPPLANI_ID"",""ACC_CODE"",""HESAPPLANI_CODE"") VALUES(""UYUMSOFT"".""ZFIND_SAGE_HESAPPLANI_ID"".NEXTVAL, :PLAN, :HESAP)";
                }
                else
                {
                    parameters = new OracleParameter[] {
                        new OracleParameter("@PLAN",textacc.Text),
                        new OracleParameter("@HESAP",textMuhasebe.Text),
                        new OracleParameter("@ID",plan.HESAPPLANI_ID)
                    };
                    sql = @"UPDATE ""UYUMSOFT"".""ZFIND_SAGE_HESAPPLANI"" SET ""ACC_CODE"" = :PLAN, ""HESAPPLANI_CODE"" = :HESAP WHERE ""HESAPPLANI_ID"" = :ID";
                }

                textId.Tag = null;
                textId.Text = "";
                textMuhasebe.Text = "";
                textacc.Text = "";
                using (OracleProvider db = new OracleProvider())
                {
                    if (!db.Execute(sql, parameters))
                    {
                        ShowError($"Kayıt hatası! {db.Message}");
                    }
                    else
                    {
                        GetPlans();
                    }
                }
            }
            catch (Exception exc)
            {
                ShowError($"Kayıt hatası! {exc.Message}\nDetay:{exc.StackTrace}");
            }
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
