﻿1.本demo支持数据库直连和WCF格式,编程上略有区别,WCF参考例子
2.生成createClass时要注意,必须用默认命名空间CreateDataTableTool,因为classControlForm DLL中有部分内容写死了,已经踩雷一次了,千万要注意.
3.目前classControlForm并不支持数据库中所有的类型.目前只支持string int decimal double 像时间,GUID类型,只在select时,在datagridview里显示即可.classxml不要配置这些类型的字段.
4.用classControlForm生成和修改的时候,并不要求classxml配置当前表里的所有字段,
一些其他字段可以直接在数据库里默认,或者在事件里面增加,例如生成时间和更新时间.update的时候,会根据主键再从数据库中获取一遍数据,所以datagridview里只要有主键就好.
已经忘了一次了,看代码才想起来.
5,配置里能设置配置多次check重复,例如用户表中的登录名不能重复,但是又不是主键的情况.
6,配置里可以配置密码模式.
7,配置里可以配置下拉模式.
8,配置里可以修改控件位置,大小等.
9,20190612在classControlForm加入了DateTime类型的支持
10.20200419进行了大的修改,WCF方式和直连的方式,在最终界面上统一,用户甚至可以不知道用的是什么方式.
11.20200426 增加了WebApi的方式,注意,这个方式要求项目framework版本462 不用这个的话可以要求项目版本4.0Clinet

实现demo,首先随便建立一个数据库,然后通过ORMTool连接数据库,将log_data,PC_config表插入,
然后可以生成Core,WebAPI,WCF三种模式的类,放到Demo中,然后再配置Core的配置文件,WebAPI服务的配置文件,WCF的配置文件
这样demo里这两个例子就能跑起来了
看例子的时候,最好看forms/PCconfig.cs这个例子,比较全 