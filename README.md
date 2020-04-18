夜战八方是我个人积累十余年的一个自用的类库.最近好好的整理了一下.删除了很多过时的以及不用的东西.<br/>
yezhanbafang.sd命名空间下大都是standard的类库是新写的,后续的东西还不多,就到数据库操作层面.但是理论上是可以跟其他的东西一起用的.<br/>
yezhanbafang.fw指的是framework下的类库,framework版本一般都是4.0 Client,基本都兼容了<br/>
yezhanbafang.fw.Core是核心的操作数据库与基础函数类库.<br/>
yezhanbafang.fw.WCF在Core的基础上包装,通过WCF操作数据库的方式.<br/>
yezhanbafang.fw.WCF.LoadBalance是将BLL层和UI层分开,进行负载均衡的方式.UI是client,BLL层在Server端.<br/>
yezhanbafang.fw.WCF.Host是WCF的服务宿主,我个人觉得寄宿在IIS和服务不爽,就得自己写个宿主.<br/>
yezhanbafang.fw.winform.classControlForm是通过配置自动实现数据CRUD的类库.<br/>
yezhanbafang.fw.winform.selectControl是实现了复杂查询的一个插件,基本上能覆盖99&的查询场景.<br/>
yezhanbafang.fw.ORMTool是个人写的以Core为核心的ORM工具以及自写的根据数据库导出相关配置的工具.<br/>
yezhanbafang.fw.WCF.AutoUpdate是在WCF的基础上实现了一个自动更新的插件,可以通过递归更新指定文件夹下的所有文件夹以及文件,并且是根据文件的MD5值比较判断文件是否更新.<br/>
yezhanbafang.fw.winform.Demo是我个人类库的集大成者,它应用了上面ORM工具生成的类以及数据库配置文件,实现了数据表的自动CRUD(连界面都自动实现了),而且能通过WCF方式和数据库直连两种方式实现.并且应用上了上面的插件和类库.<br/>
yezhanbafang.fw.MSSqlBakUp 是可以给非程序员使用的一个工具,可以用来备份Sqlserver数据库,以及实现异地备份(这里还应用到了我自写的短信平台服务,备份失败成功都有短信提醒,防止程序崩溃导致短信发送失败,只要没收到短信,也是不正常的情况)
