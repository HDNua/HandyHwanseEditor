using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandyHwanseEditor
{
    public partial class MainForm : Form
    {
        #region 상수를 정의합니다.
        const int ADDR_MONEY                 = 0x00000008;

        const int ADDR_ITEM_HERB             = 0x00000015;
        const int ADDR_ITEM_2                = 0x00000017;
        const int ADDR_ITEM_REFRESH_WATER    = 0x00000019;
        const int ADDR_ITEM_MP_RECOVERY      = 0x0000001B;
        const int ADDR_ITEM_5                = 0x0000001D;
        const int ADDR_ITEM_6                = 0x0000001F;

        const int ADDR_ATAHO_CURRENT_HP      = 0x0000007A;
        const int ADDR_ATAHO_RECOVER_HP      = 0x0000007C;
        const int ADDR_ATAHO_MAXIMUM_HP      = 0x0000007E;
        const int ADDR_ATAHO_EXPERIENCE      = 0x00000086;
        const int ADDR_ATAHO_LEVEL           = 0x00000078;
        const int ADDR_ATAHO_CURRENT_MP      = 0x00000080;
        const int ADDR_ATAHO_RECOVER_MP      = 0x00000082;
        const int ADDR_ATAHO_MAXIMUM_MP      = 0x00000084;
        const int ADDR_ATAHO_MAXIMUM_EX      = 0x00000088;
        const int ADDR_ATAHO_DEFAULT_ATK     = 0x00000130;
        const int ADDR_ATAHO_DEFAULT_DEF     = 0x00000134;
        const int ADDR_ATAHO_DEFAULT_TECH    = 0x00000136;
        const int ADDR_ATAHO_DEFAULT_QUICK   = 0x00000138;
        const int ADDR_ATAHO_DEFAULT_LUCK    = 0x0000013A;
        const int ADDR_ATAHO_ENHANCE_ATK     = 0x0000008E;
        const int ADDR_ATAHO_ENHANCE_DEF     = 0x00000092;
        const int ADDR_ATAHO_ENHANCE_TECH    = 0x00000094;
        const int ADDR_ATAHO_ENHANCE_QUICK   = 0x00000096;
        const int ADDR_ATAHO_ENHANCE_LUCK    = 0x00000098;

        const int ADDR_RINSHAN_CURRENT_HP    = 0x00000152;
        const int ADDR_RINSHAN_RECOVER_HP    = 0x00000154;
        const int ADDR_RINSHAN_MAXIMUM_HP    = 0x00000156;
        const int ADDR_RINSHAN_EXPERIENCE    = 0x0000015E;
        const int ADDR_RINSHAN_LEVEL         = 0x00000150;
        const int ADDR_RINSHAN_CURRENT_MP    = 0x00000158;
        const int ADDR_RINSHAN_RECOVER_MP    = 0x0000015A;
        const int ADDR_RINSHAN_MAXIMUM_MP    = 0x0000015C;
        const int ADDR_RINSHAN_MAXIMUM_EX    = 0x00000160;
        const int ADDR_RINSHAN_DEFAULT_ATK   = 0x00000208;
        const int ADDR_RINSHAN_DEFAULT_DEF   = 0x0000020C;
        const int ADDR_RINSHAN_DEFAULT_TECH  = 0x0000020E;
        const int ADDR_RINSHAN_DEFAULT_QUICK = 0x00000210;
        const int ADDR_RINSHAN_DEFAULT_LUCK  = 0x00000212;
        const int ADDR_RINSHAN_ENHANCE_ATK   = 0x00000166;
        const int ADDR_RINSHAN_ENHANCE_DEF   = 0x0000016A;
        const int ADDR_RINSHAN_ENHANCE_TECH  = 0x0000016C;
        const int ADDR_RINSHAN_ENHANCE_QUICK = 0x0000016E;
        const int ADDR_RINSHAN_ENHANCE_LUCK  = 0x00000170;

        const int ADDR_SMASHU_CURRENT_HP     = 0x0000022A;
        const int ADDR_SMASHU_RECOVER_HP     = 0x0000022C;
        const int ADDR_SMASHU_MAXIMUM_HP     = 0x0000022E;
        const int ADDR_SMASHU_EXPERIENCE     = 0x00000236;
        const int ADDR_SMASHU_LEVEL          = 0x00000228;
        const int ADDR_SMASHU_CURRENT_MP     = 0x00000230;
        const int ADDR_SMASHU_RECOVER_MP     = 0x00000232;
        const int ADDR_SMASHU_MAXIMUM_MP     = 0x00000234;
        const int ADDR_SMASHU_MAXIMUM_EX     = 0x00000238;
        const int ADDR_SMASHU_DEFAULT_ATK    = 0x000002E0;
        const int ADDR_SMASHU_DEFAULT_DEF    = 0x000002E4;
        const int ADDR_SMASHU_DEFAULT_TECH   = 0x000002E6;
        const int ADDR_SMASHU_DEFAULT_QUICK  = 0x000002E8;
        const int ADDR_SMASHU_DEFAULT_LUCK   = 0x000002EA;
        const int ADDR_SMASHU_ENHANCE_ATK    = 0x0000023E;
        const int ADDR_SMASHU_ENHANCE_DEF    = 0x00000242;
        const int ADDR_SMASHU_ENHANCE_TECH   = 0x00000244;
        const int ADDR_SMASHU_ENHANCE_QUICK  = 0x00000246;
        const int ADDR_SMASHU_ENHANCE_LUCK   = 0x00000248;

        const int ADDR_ATAHO_SKILL_INDIV = 0x000000CE;
        const int ADDR_ATAHO_SKILL_GROUP = 0x000000D4;
        const int ADDR_RINSHAN_SKILL_INDIV = 0x000001A6;
        const int ADDR_RINSHAN_SKILL_GROUP = 0x000001AC;
        const int ADDR_SMASHU_SKILL_INDIV = 0x0000027E;
        const int ADDR_SMASHU_SKILL_GROUP = 0x00000284;

        const int OFFSET_ATAHO_SKILL_INDIV0 = 0x0A;
        const int OFFSET_ATAHO_SKILL_INDIV1 = 0x0E;
        const int OFFSET_ATAHO_SKILL_INDIV2 = 0x12;
        const int OFFSET_ATAHO_SKILL_INDIV3 = 0x16;
        const int OFFSET_ATAHO_SKILL_INDIV4 = 0x1A;
        const int OFFSET_ATAHO_SKILL_GROUP0 = 0x23;
        const int OFFSET_ATAHO_SKILL_GROUP1 = 0x27;
        const int OFFSET_ATAHO_SKILL_GROUP2 = 0x2B;
        const int OFFSET_ATAHO_SKILL_GROUP3 = 0x2F;
        const int OFFSET_ATAHO_SKILL_GROUP4 = 0x33;

        const int OFFSET_RINSHAN_SKILL_INDIV0 = 0x09;
        const int OFFSET_RINSHAN_SKILL_INDIV1 = 0x0D;
        const int OFFSET_RINSHAN_SKILL_INDIV2 = 0x11;
        const int OFFSET_RINSHAN_SKILL_INDIV3 = 0x15;
        const int OFFSET_RINSHAN_SKILL_GROUP0 = 0x1C;
        const int OFFSET_RINSHAN_SKILL_GROUP1 = 0x20;
        const int OFFSET_RINSHAN_SKILL_GROUP2 = 0x24;
        const int OFFSET_RINSHAN_SKILL_GROUP3 = 0x28;

        const int OFFSET_SMASHU_SKILL_INDIV0 = 0x05;
        const int OFFSET_SMASHU_SKILL_INDIV1 = 0x09;
        const int OFFSET_SMASHU_SKILL_INDIV2 = 0x0D;
        const int OFFSET_SMASHU_SKILL_GROUP0 = 0x14;
        const int OFFSET_SMASHU_SKILL_GROUP1 = 0x18;
        const int OFFSET_SMASHU_SKILL_GROUP2 = 0x1C;

        #endregion



        #region 필드를 정의합니다.
        /// <summary>
        /// 
        /// </summary>
        byte[] _bytes;

        #endregion



        #region 생성자 함수를 정의합니다.
        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }


        #endregion





        #region 이벤트 핸들러를 정의합니다.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _bytes = File.ReadAllBytes(openFileDialog.FileName);

                // 
                ReadByte3(ADDR_MONEY, tbMoney);

                // 
                ReadByte(ADDR_ITEM_HERB, tbItemHerb);
                ReadByte(ADDR_ITEM_2, tbItem2);
                ReadByte(ADDR_ITEM_REFRESH_WATER, tbItemRefreshWater);
                ReadByte(ADDR_ITEM_MP_RECOVERY, tbItemMpRecovery);
                ReadByte(ADDR_ITEM_5, tbItem5);
                ReadByte(ADDR_ITEM_6, tbItem6);

                // 
                ReadByte2(ADDR_ATAHO_LEVEL, tbAtahoLevel);
                ReadByte2(ADDR_ATAHO_EXPERIENCE, tbAtahoCurrentEx);
                ReadByte2(ADDR_ATAHO_MAXIMUM_EX, tbAtahoMaximumEx);
                ReadByte2(ADDR_ATAHO_CURRENT_HP, tbAtahoCurrentHp);
                ReadByte2(ADDR_ATAHO_RECOVER_HP, tbAtahoRecoverHp);
                ReadByte2(ADDR_ATAHO_MAXIMUM_HP, tbAtahoMaximumHp);
                ReadByte2(ADDR_ATAHO_CURRENT_MP, tbAtahoCurrentMp);
                ReadByte2(ADDR_ATAHO_RECOVER_MP, tbAtahoRecoverMp);
                ReadByte2(ADDR_ATAHO_MAXIMUM_MP, tbAtahoMaximumMp);
                ReadByte2(ADDR_ATAHO_DEFAULT_ATK, tbAtahoDefaultAtk);
                ReadByte2(ADDR_ATAHO_DEFAULT_DEF, tbAtahoDefaultDef);
                ReadByte2(ADDR_ATAHO_DEFAULT_TECH, tbAtahoDefaultTech);
                ReadByte2(ADDR_ATAHO_DEFAULT_QUICK, tbAtahoDefaultQuick);
                ReadByte2(ADDR_ATAHO_DEFAULT_LUCK, tbAtahoDefaultLuck);
                ReadByte2(ADDR_ATAHO_ENHANCE_ATK, tbAtahoEnhanceAtk);
                ReadByte2(ADDR_ATAHO_ENHANCE_DEF, tbAtahoEnhanceDef);
                ReadByte2(ADDR_ATAHO_ENHANCE_TECH, tbAtahoEnhanceTech);
                ReadByte2(ADDR_ATAHO_ENHANCE_QUICK, tbAtahoEnhanceQuick);
                ReadByte2(ADDR_ATAHO_ENHANCE_LUCK, tbAtahoEnhanceLuck);

                ReadByte2(ADDR_RINSHAN_LEVEL, tbRinshanLevel);
                ReadByte2(ADDR_RINSHAN_EXPERIENCE, tbRinshanCurrentEx);
                ReadByte2(ADDR_RINSHAN_MAXIMUM_EX, tbRinshanMaximumEx);
                ReadByte2(ADDR_RINSHAN_CURRENT_HP, tbRinshanCurrentHp);
                ReadByte2(ADDR_RINSHAN_RECOVER_HP, tbRinshanRecoverHp);
                ReadByte2(ADDR_RINSHAN_MAXIMUM_HP, tbRinshanMaximumHp);
                ReadByte2(ADDR_RINSHAN_CURRENT_MP, tbRinshanCurrentMp);
                ReadByte2(ADDR_RINSHAN_RECOVER_MP, tbRinshanRecoverMp);
                ReadByte2(ADDR_RINSHAN_MAXIMUM_MP, tbRinshanMaximumMp);
                ReadByte2(ADDR_RINSHAN_DEFAULT_ATK, tbRinshanDefaultAtk);
                ReadByte2(ADDR_RINSHAN_DEFAULT_DEF, tbRinshanDefaultDef);
                ReadByte2(ADDR_RINSHAN_DEFAULT_TECH, tbRinshanDefaultTech);
                ReadByte2(ADDR_RINSHAN_DEFAULT_QUICK, tbRinshanDefaultQuick);
                ReadByte2(ADDR_RINSHAN_DEFAULT_LUCK, tbRinshanDefaultLuck);
                ReadByte2(ADDR_RINSHAN_ENHANCE_ATK, tbRinshanEnhanceAtk);
                ReadByte2(ADDR_RINSHAN_ENHANCE_DEF, tbRinshanEnhanceDef);
                ReadByte2(ADDR_RINSHAN_ENHANCE_TECH, tbRinshanEnhanceTech);
                ReadByte2(ADDR_RINSHAN_ENHANCE_QUICK, tbRinshanEnhanceQuick);
                ReadByte2(ADDR_RINSHAN_ENHANCE_LUCK, tbRinshanEnhanceLuck);

                ReadByte2(ADDR_SMASHU_LEVEL, tbSmashuLevel);
                ReadByte2(ADDR_SMASHU_EXPERIENCE, tbSmashuCurrentEx);
                ReadByte2(ADDR_SMASHU_MAXIMUM_EX, tbSmashuMaximumEx);
                ReadByte2(ADDR_SMASHU_CURRENT_HP, tbSmashuCurrentHp);
                ReadByte2(ADDR_SMASHU_RECOVER_HP, tbSmashuRecoverHp);
                ReadByte2(ADDR_SMASHU_MAXIMUM_HP, tbSmashuMaximumHp);
                ReadByte2(ADDR_SMASHU_CURRENT_MP, tbSmashuCurrentMp);
                ReadByte2(ADDR_SMASHU_RECOVER_MP, tbSmashuRecoverMp);
                ReadByte2(ADDR_SMASHU_MAXIMUM_MP, tbSmashuMaximumMp);
                ReadByte2(ADDR_SMASHU_DEFAULT_ATK, tbSmashuDefaultAtk);
                ReadByte2(ADDR_SMASHU_DEFAULT_DEF, tbSmashuDefaultDef);
                ReadByte2(ADDR_SMASHU_DEFAULT_TECH, tbSmashuDefaultTech);
                ReadByte2(ADDR_SMASHU_DEFAULT_QUICK, tbSmashuDefaultQuick);
                ReadByte2(ADDR_SMASHU_DEFAULT_LUCK, tbSmashuDefaultLuck);
                ReadByte2(ADDR_SMASHU_ENHANCE_ATK, tbSmashuEnhanceAtk);
                ReadByte2(ADDR_SMASHU_ENHANCE_DEF, tbSmashuEnhanceDef);
                ReadByte2(ADDR_SMASHU_ENHANCE_TECH, tbSmashuEnhanceTech);
                ReadByte2(ADDR_SMASHU_ENHANCE_QUICK, tbSmashuEnhanceQuick);
                ReadByte2(ADDR_SMASHU_ENHANCE_LUCK, tbSmashuEnhanceLuck);

                // 
                ReadSkills(
                    ADDR_ATAHO_SKILL_INDIV,
                    new int[] { OFFSET_ATAHO_SKILL_INDIV0, OFFSET_ATAHO_SKILL_INDIV1, OFFSET_ATAHO_SKILL_INDIV2, OFFSET_ATAHO_SKILL_INDIV3, OFFSET_ATAHO_SKILL_INDIV4 },
                    new GroupBox[] { gbAtahoSkillIndiv0, gbAtahoSkillIndiv1, gbAtahoSkillIndiv2, gbAtahoSkillIndiv3, gbAtahoSkillIndiv4 }
                    );
                ReadSkills(
                    ADDR_ATAHO_SKILL_GROUP,
                    new int[] { OFFSET_ATAHO_SKILL_GROUP0, OFFSET_ATAHO_SKILL_GROUP1, OFFSET_ATAHO_SKILL_GROUP2, OFFSET_ATAHO_SKILL_GROUP3, OFFSET_ATAHO_SKILL_GROUP4 },
                    new GroupBox[] { gbAtahoSkillGroup0, gbAtahoSkillGroup1, gbAtahoSkillGroup2, gbAtahoSkillGroup3, gbAtahoSkillGroup4 }
                    );
                ReadSkills(
                    ADDR_RINSHAN_SKILL_INDIV,
                    new int[] { OFFSET_RINSHAN_SKILL_INDIV0, OFFSET_RINSHAN_SKILL_INDIV1, OFFSET_RINSHAN_SKILL_INDIV2, OFFSET_RINSHAN_SKILL_INDIV3 },
                    new GroupBox[] { gbRinshanSkillIndiv0, gbRinshanSkillIndiv1, gbRinshanSkillIndiv2, gbRinshanSkillIndiv3 }
                    );
                ReadSkills(
                    ADDR_RINSHAN_SKILL_GROUP,
                    new int[] { OFFSET_RINSHAN_SKILL_GROUP0, OFFSET_RINSHAN_SKILL_GROUP1, OFFSET_RINSHAN_SKILL_GROUP2, OFFSET_RINSHAN_SKILL_GROUP3 },
                    new GroupBox[] { gbRinshanSkillGroup0, gbRinshanSkillGroup1, gbRinshanSkillGroup2, gbRinshanSkillGroup3 }
                    );
                ReadSkills(
                    ADDR_SMASHU_SKILL_INDIV,
                    new int[] { OFFSET_SMASHU_SKILL_INDIV0, OFFSET_SMASHU_SKILL_INDIV1, OFFSET_SMASHU_SKILL_INDIV2 },
                    new GroupBox[] { gbSmashuSkillIndiv0, gbSmashuSkillIndiv1, gbSmashuSkillIndiv2 }
                    );
                ReadSkills(
                    ADDR_SMASHU_SKILL_GROUP,
                    new int[] { OFFSET_SMASHU_SKILL_GROUP0, OFFSET_SMASHU_SKILL_GROUP1, OFFSET_SMASHU_SKILL_GROUP2 },
                    new GroupBox[] { gbSmashuSkillGroup0, gbSmashuSkillGroup1, gbSmashuSkillGroup2 }
                    );

                // 
                mainStatusStripLabel.Text = string.Format("read at {0}", DateTime.Now);
                저장ToolStripMenuItem.Enabled = true;
                다른이름으로저장ToolStripMenuItem.Enabled = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGameData(openFileDialog.FileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 다른이름으로저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveGameData(saveFileDialog.FileName);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 닫기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 만든사람ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        #endregion





        #region 보조 메서드를 정의합니다.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        void SaveGameData(string filename)
        {
            // 
            WriteByte3(ADDR_MONEY, tbMoney);

            // 
            WriteByte(ADDR_ITEM_HERB, tbItemHerb);
            WriteByte(ADDR_ITEM_2, tbItem2);
            WriteByte(ADDR_ITEM_REFRESH_WATER, tbItemRefreshWater);
            WriteByte(ADDR_ITEM_MP_RECOVERY, tbItemMpRecovery);
            WriteByte(ADDR_ITEM_5, tbItem5);
            WriteByte(ADDR_ITEM_6, tbItem6);

            // 
            WriteByte2(ADDR_ATAHO_LEVEL, tbAtahoLevel);
            WriteByte2(ADDR_ATAHO_EXPERIENCE, tbAtahoCurrentEx);
            WriteByte2(ADDR_ATAHO_MAXIMUM_EX, tbAtahoMaximumEx);
            WriteByte2(ADDR_ATAHO_CURRENT_HP, tbAtahoCurrentHp);
            WriteByte2(ADDR_ATAHO_RECOVER_HP, tbAtahoRecoverHp);
            WriteByte2(ADDR_ATAHO_MAXIMUM_HP, tbAtahoMaximumHp);
            WriteByte2(ADDR_ATAHO_CURRENT_MP, tbAtahoCurrentMp);
            WriteByte2(ADDR_ATAHO_RECOVER_MP, tbAtahoRecoverMp);
            WriteByte2(ADDR_ATAHO_MAXIMUM_MP, tbAtahoMaximumMp);
            WriteByte2(ADDR_ATAHO_DEFAULT_ATK, tbAtahoDefaultAtk);
            WriteByte2(ADDR_ATAHO_DEFAULT_DEF, tbAtahoDefaultDef);
            WriteByte2(ADDR_ATAHO_DEFAULT_TECH, tbAtahoDefaultTech);
            WriteByte2(ADDR_ATAHO_DEFAULT_QUICK, tbAtahoDefaultQuick);
            WriteByte2(ADDR_ATAHO_DEFAULT_LUCK, tbAtahoDefaultLuck);
            WriteByte2(ADDR_ATAHO_ENHANCE_ATK, tbAtahoEnhanceAtk);
            WriteByte2(ADDR_ATAHO_ENHANCE_DEF, tbAtahoEnhanceDef);
            WriteByte2(ADDR_ATAHO_ENHANCE_TECH, tbAtahoEnhanceTech);
            WriteByte2(ADDR_ATAHO_ENHANCE_QUICK, tbAtahoEnhanceQuick);
            WriteByte2(ADDR_ATAHO_ENHANCE_LUCK, tbAtahoEnhanceLuck);

            WriteByte2(ADDR_RINSHAN_LEVEL, tbRinshanLevel);
            WriteByte2(ADDR_RINSHAN_EXPERIENCE, tbRinshanCurrentEx);
            WriteByte2(ADDR_RINSHAN_MAXIMUM_EX, tbRinshanMaximumEx);
            WriteByte2(ADDR_RINSHAN_CURRENT_HP, tbRinshanCurrentHp);
            WriteByte2(ADDR_RINSHAN_RECOVER_HP, tbRinshanRecoverHp);
            WriteByte2(ADDR_RINSHAN_MAXIMUM_HP, tbRinshanMaximumHp);
            WriteByte2(ADDR_RINSHAN_CURRENT_MP, tbRinshanCurrentMp);
            WriteByte2(ADDR_RINSHAN_RECOVER_MP, tbRinshanRecoverMp);
            WriteByte2(ADDR_RINSHAN_MAXIMUM_MP, tbRinshanMaximumMp);
            WriteByte2(ADDR_RINSHAN_DEFAULT_ATK, tbRinshanDefaultAtk);
            WriteByte2(ADDR_RINSHAN_DEFAULT_DEF, tbRinshanDefaultDef);
            WriteByte2(ADDR_RINSHAN_DEFAULT_TECH, tbRinshanDefaultTech);
            WriteByte2(ADDR_RINSHAN_DEFAULT_QUICK, tbRinshanDefaultQuick);
            WriteByte2(ADDR_RINSHAN_DEFAULT_LUCK, tbRinshanDefaultLuck);
            WriteByte2(ADDR_RINSHAN_ENHANCE_ATK, tbRinshanEnhanceAtk);
            WriteByte2(ADDR_RINSHAN_ENHANCE_DEF, tbRinshanEnhanceDef);
            WriteByte2(ADDR_RINSHAN_ENHANCE_TECH, tbRinshanEnhanceTech);
            WriteByte2(ADDR_RINSHAN_ENHANCE_QUICK, tbRinshanEnhanceQuick);
            WriteByte2(ADDR_RINSHAN_ENHANCE_LUCK, tbRinshanEnhanceLuck);

            WriteByte2(ADDR_SMASHU_LEVEL, tbSmashuLevel);
            WriteByte2(ADDR_SMASHU_EXPERIENCE, tbSmashuCurrentEx);
            WriteByte2(ADDR_SMASHU_MAXIMUM_EX, tbSmashuMaximumEx);
            WriteByte2(ADDR_SMASHU_CURRENT_HP, tbSmashuCurrentHp);
            WriteByte2(ADDR_SMASHU_RECOVER_HP, tbSmashuRecoverHp);
            WriteByte2(ADDR_SMASHU_MAXIMUM_HP, tbSmashuMaximumHp);
            WriteByte2(ADDR_SMASHU_CURRENT_MP, tbSmashuCurrentMp);
            WriteByte2(ADDR_SMASHU_RECOVER_MP, tbSmashuRecoverMp);
            WriteByte2(ADDR_SMASHU_MAXIMUM_MP, tbSmashuMaximumMp);
            WriteByte2(ADDR_SMASHU_DEFAULT_ATK, tbSmashuDefaultAtk);
            WriteByte2(ADDR_SMASHU_DEFAULT_DEF, tbSmashuDefaultDef);
            WriteByte2(ADDR_SMASHU_DEFAULT_TECH, tbSmashuDefaultTech);
            WriteByte2(ADDR_SMASHU_DEFAULT_QUICK, tbSmashuDefaultQuick);
            WriteByte2(ADDR_SMASHU_DEFAULT_LUCK, tbSmashuDefaultLuck);
            WriteByte2(ADDR_SMASHU_ENHANCE_ATK, tbSmashuEnhanceAtk);
            WriteByte2(ADDR_SMASHU_ENHANCE_DEF, tbSmashuEnhanceDef);
            WriteByte2(ADDR_SMASHU_ENHANCE_TECH, tbSmashuEnhanceTech);
            WriteByte2(ADDR_SMASHU_ENHANCE_QUICK, tbSmashuEnhanceQuick);
            WriteByte2(ADDR_SMASHU_ENHANCE_LUCK, tbSmashuEnhanceLuck);

            // 
            UpdateSkills(
                ADDR_ATAHO_SKILL_INDIV,
                new int[] { OFFSET_ATAHO_SKILL_INDIV0, OFFSET_ATAHO_SKILL_INDIV1, OFFSET_ATAHO_SKILL_INDIV2, OFFSET_ATAHO_SKILL_INDIV3, OFFSET_ATAHO_SKILL_INDIV4 },
                new GroupBox[] { gbAtahoSkillIndiv0, gbAtahoSkillIndiv1, gbAtahoSkillIndiv2, gbAtahoSkillIndiv3, gbAtahoSkillIndiv4 }
                );
            UpdateSkills(
                ADDR_ATAHO_SKILL_GROUP,
                new int[] { OFFSET_ATAHO_SKILL_GROUP0, OFFSET_ATAHO_SKILL_GROUP1, OFFSET_ATAHO_SKILL_GROUP2, OFFSET_ATAHO_SKILL_GROUP3, OFFSET_ATAHO_SKILL_GROUP4 },
                new GroupBox[] { gbAtahoSkillGroup0, gbAtahoSkillGroup1, gbAtahoSkillGroup2, gbAtahoSkillGroup3, gbAtahoSkillGroup4 }
                );
            UpdateSkills(
                ADDR_RINSHAN_SKILL_INDIV,
                new int[] { OFFSET_RINSHAN_SKILL_INDIV0, OFFSET_RINSHAN_SKILL_INDIV1, OFFSET_RINSHAN_SKILL_INDIV2, OFFSET_RINSHAN_SKILL_INDIV3 },
                new GroupBox[] { gbRinshanSkillIndiv0, gbRinshanSkillIndiv1, gbRinshanSkillIndiv2, gbRinshanSkillIndiv3 }
                );
            UpdateSkills(
                ADDR_RINSHAN_SKILL_GROUP,
                new int[] { OFFSET_RINSHAN_SKILL_GROUP0, OFFSET_RINSHAN_SKILL_GROUP1, OFFSET_RINSHAN_SKILL_GROUP2, OFFSET_RINSHAN_SKILL_GROUP3 },
                new GroupBox[] { gbRinshanSkillGroup0, gbRinshanSkillGroup1, gbRinshanSkillGroup2, gbRinshanSkillGroup3 }
                );
            UpdateSkills(
                ADDR_SMASHU_SKILL_INDIV,
                new int[] { OFFSET_SMASHU_SKILL_INDIV0, OFFSET_SMASHU_SKILL_INDIV1, OFFSET_SMASHU_SKILL_INDIV2 },
                new GroupBox[] { gbSmashuSkillIndiv0, gbSmashuSkillIndiv1, gbSmashuSkillIndiv2 }
                );
            UpdateSkills(
                ADDR_SMASHU_SKILL_GROUP,
                new int[] { OFFSET_SMASHU_SKILL_GROUP0, OFFSET_SMASHU_SKILL_GROUP1, OFFSET_SMASHU_SKILL_GROUP2 },
                new GroupBox[] { gbSmashuSkillGroup0, gbSmashuSkillGroup1, gbSmashuSkillGroup2 }
                );

            // 
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(filename));
            bw.Write(_bytes);
            bw.Close();

            // 
            mainStatusStripLabel.Text = string.Format("done at {0}", DateTime.Now);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="dstTextBox"></param>
        void ReadByte(int addr, TextBox dstTextBox)
        {
            byte[] bytes = new byte[] { _bytes[addr], 0, 0, 0 };
            dstTextBox.Text = BitConverter.ToInt32(bytes, 0).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="dstTextBox"></param>
        void ReadByte2(int addr, TextBox dstTextBox)
        {
            byte[] bytes = new byte[] { _bytes[addr], _bytes[addr + 1], 0, 0 };
            dstTextBox.Text = BitConverter.ToInt32(bytes, 0).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="dstTextBox"></param>
        void ReadByte3(int addr, TextBox dstTextBox)
        {
            byte[] bytes = new byte[] { _bytes[addr], _bytes[addr + 1], _bytes[addr + 2], 0 };
            dstTextBox.Text = BitConverter.ToInt32(bytes, 0).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="offsets"></param>
        /// <param name="gbSkills"></param>
        void ReadSkills(int addr, int[] offsets, GroupBox[] gbSkills)
        {
            for (int i = 0; i < gbSkills.Length; ++i)
            {
                GroupBox gbSkill = gbSkills[i];
                RadioButton[] radioButtons = GetRadioButtons(gbSkill);

                // 
                int a = _bytes[addr + i];
                for (int j = 0; j < offsets.Length; ++j)
                {
                    if (offsets[j] <= a && a <= offsets[j] + 3)
                    {
                        radioButtons[a - offsets[j] + 1].Checked = true;
                        break;
                    }
                    else
                    {
                        radioButtons[0].Checked = true;
                    }
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="srcTextBox"></param>
        void WriteByte(int addr, TextBox srcTextBox)
        {
            _bytes[addr] = byte.Parse(srcTextBox.Text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="srcTextBox"></param>
        void WriteByte2(int addr, TextBox srcTextBox)
        {
            short value = short.Parse(srcTextBox.Text);
            byte[] bytes = BitConverter.GetBytes(value);
            _bytes[addr] = bytes[0];
            _bytes[addr + 1] = bytes[1];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="srcTextBox"></param>
        void WriteByte3(int addr, TextBox srcTextBox)
        {
            int value = int.Parse(srcTextBox.Text);
            byte[] bytes = BitConverter.GetBytes(value);
            _bytes[addr] = bytes[0];
            _bytes[addr + 1] = bytes[1];
            _bytes[addr + 2] = bytes[2];
        }

        /// <summary>
        /// 
        /// </summary>
        byte[][] GetSkillsMap(int[] offsets, GroupBox[] gbSkills)
        {
            int len = gbSkills.Length;
            byte[][] skill_status = new byte[len][];
            for (int i = 0; i < len; ++i)
            {
                GroupBox gbSkill = gbSkills[i];
                skill_status[i] = new byte[5] { 0, 0, 0, 0, 0 };

                RadioButton[] radioButtons = GetRadioButtons(gbSkill);
                if (radioButtons[0].Checked)
                {
                    int v = 0;
                    skill_status[i][0] = byte.Parse(v.ToString());
                }
                if (radioButtons[1].Checked)
                {
                    int v = offsets[i];
                    skill_status[i][1] = byte.Parse(v.ToString());
                }
                if (radioButtons[2].Checked)
                {
                    int v = offsets[i] + 1;
                    skill_status[i][2] = byte.Parse(v.ToString());
                }
                if (radioButtons[3].Checked)
                {
                    int v = offsets[i] + 2;
                    skill_status[i][3] = byte.Parse(v.ToString());
                }
                if (radioButtons[4].Checked)
                {
                    int v = offsets[i] + 3;
                    skill_status[i][4] = byte.Parse(v.ToString());
                }
            }

            // 
            return skill_status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="offsets"></param>
        /// <param name="gbSkills"></param>
        void UpdateSkills(int baseAddr, int[] offsets, GroupBox[] gbSkills)
        {
            int addr = baseAddr;
            byte[][] indivSkillsMap = GetSkillsMap(offsets, gbSkills);
            for (int i = 0; i < indivSkillsMap.Length; ++i)
            {
                byte[] skillMap = indivSkillsMap[i];
                for (int j = 0; j < 5; ++j)
                {
                    if (skillMap[j] != 0)
                    {
                        _bytes[addr++] = skillMap[j];
                    }
                }
            }

            // 
            for (int i = 0, diff = indivSkillsMap.Length - (addr - baseAddr); i < diff; ++i)
            {
                _bytes[addr++] = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gbSkill"></param>
        /// <returns></returns>
        RadioButton[] GetRadioButtons(GroupBox gbSkill)
        {
            var controls = gbSkill.Controls.Cast<Control>();
            Control[] control_array = controls.ToArray();
            List<RadioButton> radioButtons = new List<RadioButton>();
            foreach (var control in control_array)
            {
                if (control is RadioButton)
                {
                    radioButtons.Add((RadioButton)control);
                }
            }
            radioButtons.Sort((x, y) => { return x.TabIndex - y.TabIndex; });
            return radioButtons.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        #endregion

    }
}
