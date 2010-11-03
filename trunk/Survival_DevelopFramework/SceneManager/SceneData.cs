using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Survival_DevelopFramework.Helpers;
using Survival_DevelopFramework.Items.PhysicItems;
using Survival_DevelopFramework.Items;
using Survival_DevelopFramework.InputSystem;
using Microsoft.Xna.Framework.Input;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Factories;
using Survival_DevelopFramework.PhysicsSystem;
using Microsoft.Xna.Framework;
using Survival_DevelopFramework.ItemDatas;

namespace Survival_DevelopFramework.SceneManager
{
    /// <summary>
    /// Scene 数据类
    /// </summary>
    public class SceneData
    {
        #region Constent
        private const string ContentDir = "Content//Scenes";
        private const string Extension = "scn";
        #endregion

        #region Variables
        #region Player
        #endregion
        #region Terrain
        public List<TerrainData> terrainDataList = new List<TerrainData>();
        public List<TiledTerrainData> tiledTerrainDataList = new List<TiledTerrainData>();
        #endregion
        #region ActiveItem
       // public List<PhysicsItem> activeItems;
        #endregion
        #region Trigger
       // public List<Trigger> triggers; 
        #endregion
        #region Movie
        #endregion
        #region Forground
        #endregion
        #region Background
        public List<BackgroundData> backgroundDataList = new List<BackgroundData>();
        #endregion
        #endregion

        #region Load Data
        /// <summary>
        /// 读取Scene文件
        /// </summary>
        static public SceneData Load(String setFilename)
        {
            // 读取文件
            StreamReader file = new StreamReader(LoadHelper.LoadFileStream(ContentDir + "\\" + setFilename + "." + Extension));
            // 将数据读入对象
            SceneData loadSceneData = (SceneData)
                new XmlSerializer(typeof(SceneData)).Deserialize(file.BaseStream);
            // 关闭文件
            file.Close();
            // 返回反序列化数据
            return loadSceneData;
        }
        #endregion

        #region UnitTest
        public static void UnitTest()
        {
            TestGame.StartTest("SceneData 载入测试 F1-Terrain F2-TiledTerrain",
                null,
                delegate
                {
                    SceneMgr.Instance.ChangeScene("TestSceneYard");
                },
                delegate
                {
                    if(InputKeyboards.isKeyJustPress(Keys.F1))
                    {
                        SceneMgr.Instance.ChangeScene("TestSceneYard");

                        Vector2 roleSize = new Vector2(32, 48);
                        float runforce = 1000.0f;
                        float jumpforce = 3000.0f;
                        float maxRunForce = 50;
                        Role role = new Role("./Roles/soldier", roleSize, 10, 32, 48, 17, runforce, jumpforce, maxRunForce);
                        role.Position = new Vector2(400, 100);
                        role.origin = new Vector2(16, 24);
                        role.animSeqList.Add(new AnimItem.AnimSequence("Free", 40, 6, 8, true, true));
                        role.animSeqList.Add(new AnimItem.AnimSequence("Running", 0, 8, 8, true, true));
                        role.animSeqList.Add(new AnimItem.AnimSequence("Jumping", 8, 9, 4, true, false));
                        role.animSeqList.Add(new AnimItem.AnimSequence("UsingItem", 17, 8, 7, true, false));
                        role.animSeqList.Add(new AnimItem.AnimSequence("UsingGun", 24, 13, 12, true, false));
                        role.animSeqList.Add(new AnimItem.AnimSequence("UsingHook", 37, 1, 8, true, false));
                        role.PlaySeq("Free");
                        SceneMgr.Instance.AddItem(role);
                    }
                    else if (InputKeyboards.isKeyJustPress(Keys.F2))
                    {
                        SceneMgr.Instance.ChangeScene("TestSceneRoom");

                        Vector2 roleSize = new Vector2(32, 48);
                        float runforce = 1000.0f;
                        float jumpforce = 3000.0f;
                        float maxRunForce = 50;
                        Role role = new Role("./Roles/soldier", roleSize, 10, 32, 48, 17, runforce, jumpforce, maxRunForce);
                        role.Position = new Vector2(400, 100);
                        role.origin = new Vector2(16, 24);
                        role.animSeqList.Add(new AnimItem.AnimSequence("Free", 40, 6, 8, true, true));
                        role.animSeqList.Add(new AnimItem.AnimSequence("Running", 0, 8, 8, true, true));
                        role.animSeqList.Add(new AnimItem.AnimSequence("Jumping", 8, 9, 4, true, false));
                        role.animSeqList.Add(new AnimItem.AnimSequence("UsingItem", 17, 8, 7, true, false));
                        role.animSeqList.Add(new AnimItem.AnimSequence("UsingGun", 24, 13, 12, true, false));
                        role.animSeqList.Add(new AnimItem.AnimSequence("UsingHook", 37, 1, 8, true, false));
                        role.PlaySeq("Free");
                        SceneMgr.Instance.AddItem(role);
                    }
                },
                delegate
                {
                }
            );
        }
        #endregion
    }
}
