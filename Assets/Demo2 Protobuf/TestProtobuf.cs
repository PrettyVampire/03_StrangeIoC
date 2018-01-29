using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.IO;

public class TestProtobuf : MonoBehaviour
{
    
	void Start () {

        #region 序列化
        //User user = new User();
        //FileStream fs = File.Create(Application.dataPath + "/user.bin");
        //Debug.Log(Application.dataPath + "/user.bin");
        //Serializer.Serialize<User>(fs, user);
        //fs.Close();

        ////创建的数据流 自动关闭
        //using (var fs = File.Create(Application.dataPath + "/user.bin"))
        //{
        //    Serializer.Serialize<User>(fs, user);
        //}
        #endregion


        #region 反序列化
        User user = null;
        using (var fs = File.OpenRead(Application.dataPath + "/user.bin"))
        {
            user = Serializer.Deserialize<User>(fs);
        }

        print(user.id);
        print(user.userName);
        #endregion
        
    }
	
}
