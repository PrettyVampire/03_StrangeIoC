using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;


[ProtoContract]//声明为protobuf的数据来传输
public class User
{
    //序列化必须制定一个tag 便于二进制中识别
    [ProtoMember(1)]
    public int id = 3;
    [ProtoMember(2)]
    public string userName = "monkey";
    [ProtoMember(3)]
    public string password = "123456";
    [ProtoMember(4)]
    public int level = 10;
    [ProtoMember(5)]
    public userType _userType = userType.master;
    
    public enum userType
    {
        master,
        warrior
    }

    

}
