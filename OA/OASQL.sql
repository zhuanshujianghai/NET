if db_id('oadb') is null
create database oadb
go
use oadb
--权限管理-管理员表
create table oa_power_ausers
(
	id bigint primary key identity(1,1),
	username nvarchar(20) unique not null,--用户名
	[password] nvarchar(30) not null,--密码
	userword nvarchar(30) not null,--口令
	roleid bigint not null,--角色id
	[status] int not null,--状态
	addtime datetime not null--添加时间
)
--权限管理-管理员详情表
create table oa_power_ausersinfos
(
	id bigint primary key identity(1,1),
	auserid bigint not null,--管理员id
	realname nvarchar(10) not null,--真实姓名
	qq varchar(20) not null,--QQ号码
	email varchar(50) not null,--邮箱
	phone varchar(20) not null,--联系电话
)
--权限管理-菜单表
create table oa_power_pagemenu
(
	id bigint primary key identity(1,1),
	name nvarchar(10) not null,--菜单名称
	url nvarchar(100) not null,--菜单路径
	image nvarchar(100) not null,--菜单图片
	parentid bigint not null,--父级id
	remark nvarchar(50) not null,--备注
	status int not null,--状态（0为正常，1为禁用）
	ispage int not null--是否为页面（0不为，1为）
)
--权限管理-角色表
create table oa_power_roles
(
	id bigint primary key identity(1,1),
	rolename nvarchar(20) not null,--角色名称
	roleinfo nvarchar(50) not null--角色详情
)
--权限管理-功能表
create table oa_power_functions
(
	id bigint primary key identity(1,1),
	functionname nvarchar(20) not null,--功能名称
	functioninfo nvarchar(50) not null,--功能详情
	parentid bigint not null--父级id
)
--权限管理-角色功能表
create table oa_power_rolesfunctions
(
	id bigint primary key identity(1,1),
	roleid bigint not null,--角色id
	functionid bigint not null,--功能id
	status int not null --状态（0为未选择，1为已选择）
)
--权限管理-Action表
create table oa_power_actions
(
	id bigint primary key identity(1,1),
	actionname nvarchar(100) not null,--action名称
	actioninfo nvarchar(50) not null--action详情
)
--权限管理-功能Action表
create table oa_power_functionsactions
(
	id bigint primary key identity(1,1),
	functionid bigint not null,--功能id
	actionid bigint not null,--actionid
	status int not null--状态（0未选中，1已选中）
)
--权限管理-权限日志表
create table oa_power_powerlogs
(
	id bigint primary key identity(1,1),
	tablename nvarchar(50) not null,--表名
	type int not null,--操作类型，1增2删3改
	content nvarchar(max) not null,--内容
	auserid bigint not null,--管理员id
	addip nvarchar(20) not null,--操作人ip
	addtime datetime not null--操作时间
)
--权限管理-操作日志表
create table oa_power_dologs
(
	id bigint primary key identity(1,1),
	auserid bigint not null,--管理员id
	type nvarchar(10) not null,--类型
	addip nvarchar(20) not null,--操作人ip
	addtime datetime not null,--操作时间
	doinfo nvarchar(50) not null--操作详情
)
--权限管理-管理员登录表
create table oa_power_auserslogins
(
	id bigint primary key identity(1,1),
	auserid bigint not null,--管理员id
	addip nvarchar(20) not null,--登陆ip
	addtime datetime not null,--添加时间
	dotime datetime not null,--操作时间
	cookie nvarchar(100) not null,--cookie字符串
	expiretime int not null --过期时间（单位：分钟）
)
--前台权限管理-菜单表
create table oa_userpower_pagemenu
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	name nvarchar(10) not null,--菜单名称
	url nvarchar(100) not null,--菜单路径
	image nvarchar(100) not null,--菜单图片
	parentid bigint not null,--父级id
	remark nvarchar(50) not null,--备注
	status int not null,--状态（-1为删除，0为正常，1为禁用）
	ispage int not null--是否为页面（0不为，1为）
)
--前台权限管理-功能表
create table oa_userpower_functions
(
	id bigint primary key identity(1,1),
	functionname nvarchar(20) not null,--功能名称
	functioninfo nvarchar(50) not null,--功能详情
	parentid bigint not null--父级id
)
--前台权限管理-职位功能表
create table oa_userpower_positionsfunctions
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	positionid bigint not null,--职位id
	functionid bigint not null,--功能id
	status int not null--状态（-1未授权，0未选中，1已选中）
)
--前台权限管理-Action表
create table oa_userpower_actions
(
	id bigint primary key identity(1,1),
	actionname nvarchar(20) not null,--action名称
	actioninfo nvarchar(50) not null--Action详情
)
--前台权限管理-功能Action表
create table oa_userpower_functionsactions
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	functionid bigint not null,--功能id
	actionid bigint not null,--actionid
	status int not null--状态（-1未授权，0未选中，1已选中）
)
--前台权限管理-权限日志表
create table oa_userpower_powerlogs
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	tablename nvarchar(50) not null,--表名
	type int not null,--操作类型
	content nvarchar(100) not null,--内容
	staffid bigint not null,--员工id
	staffname nvarchar(20) not null,--用户名
	addtime datetime not null --操作时间
)
--人员管理-公司表
create table oa_member_companys
(
	id bigint primary key identity(1,1),
	companyname nvarchar(50) not null,--公司名称
	companyinfo nvarchar(100) not null,--公司详情
	status int not null,--状态
	addtime datetime not null--创建时间
)
--人员管理-部门表
create table oa_member_departments
(
	id bigint primary key identity(1,1),
	departmentname nvarchar(50) not null,--部门名称
	departmentinfo nvarchar(100) not null,--部门详情
	companyid bigint not null,--公司id
	status int not null,--状态
	addtime datetime not null--创建时间
)
--人员管理-员工表
create table oa_member_staffs
(
	id bigint primary key identity(1,1),
	username nvarchar(20) not null unique,--用户名
	email nvarchar(50) not null unique,--邮箱
	phone varchar(20) not null unique,--手机
	password nvarchar(30) not null,--密码
	userword nvarchar(20) not null,--口令
	status int not null,--状态
	departmentid bigint not null,--部门id
	companyid bigint not null,--公司id
	isall int not null,--是否查看全部（0为不能，1为能）
	isachievement int not null,--是否绩效考核（0为无，1为有）
	addtime datetime not null--添加时间
)
--人员管理-员工详情表
create table oa_member_staffsinfos
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--员工id
	realname nvarchar(10) not null,--真实姓名
	sex int not null,--性别0女1男
	age int not null,--年龄
	idcard varchar(20) not null,--身份证号码
	address nvarchar(100) not null--居住地址
)
--人员管理-操作日志表
create table oa_member_dologs
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--员工id
	type nvarchar(10) not null,--类型
	addip nvarchar(20) not null,--操作ip
	addtime datetime not null,--操作时间
	doinfo nvarchar(50) not null--操作详情
)
--人员管理-员工登录表
create table oa_member_staffslogins
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--员工id
	addip varchar(20) not null,--登陆ip
	addtime datetime not null,--登陆时间
	dotime datetime not null,--操作时间
	cookie varchar(100) not null,--cookie字符串
	expiretime int not null --过期时间单位：分钟
)
--人员管理-职位表
create table oa_member_positions
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	departmentid bigint not null,--部门id，--2016/5/18新增
	positionname nvarchar(10) not null,--职位名称
	positioninfo nvarchar(50) not null,--职位详情
	parentid bigint not null,--父级id，--2016/5/18新增
	level int not null, --级别等级，1最大2次之，--2016/5/18新增
	status int not null,--状态
	sort int not null--排序
)
--人员管理-员工职位表
create table oa_member_staffspositions
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	staffid bigint not null,--员工id
	positionid bigint not null,--职位id
)
--项目管理-项目表
create table oa_project_projects
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	staffid bigint not null,--创建人id
	person_responsibleid bigint not null,--负责人id
	projectname nvarchar(20) not null,--项目名称
	projectinfo nvarchar(4000) not null,--项目详情
	progress decimal not null,--进度
	status int not null,--状态（0新建1进行中2已完成）
	addtime datetime not null,--添加时间
	expectbegintime datetime not null,--预计开始时间
	realitybegintime datetime not null,--实际开始时间
	updatetime datetime not null,--更新时间
	expectfinishtime datetime not null,--预计完成时间
	realityfinishtime datetime not null--实际完成时间
)
--项目管理-项目人员表
create table oa_project_projectsstaffs
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	projectid bigint not null,--项目id
	staffid bigint not null,--人员id
	rolename nvarchar(10) not null,--项目角色名称
	status int not null--状态（-1删除，0正常）
)
--项目管理-项目Bug表
create table oa_project_projectsbugs
(
	id bigint primary key identity(1,1),
	projectid bigint not null,--项目id
	title nvarchar(50) not null,--bug标题
	type int not null,--bug类型(代码错误 = 0, 界面优化 = 1, 设计缺陷 = 2, 配置相关 = 3, 安装部署 = 4, 安全相关 = 5, 性能问题 = 6, 标准规范 = 7, 测试脚本 = 8, 其他 = 9)
	degree int not null,--优先程度（0普通1重要2紧急，默认0）
	reproducestep nvarchar(4000) not null,--重现步骤
	dostep nvarchar(4000) not null,--操作步骤
	writer bigint not null,--提出者
	editor bigint not null,--修改者
	status int not null,--状态（0未确认1未解决2待关闭3已完成）
	addtime datetime not null,--添加时间
	confirmtime datetime not null,--确认时间
	solvetime datetime not null,--解决时间
	closetime datetime not null--关闭时间
)
--审批流程管理-审批项目表
create table oa_approvalprocess_projects
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	creator	bigint not null,--创建者，对应员工表id
	addtime datetime not null,--创建时间
	projectname nvarchar(20) not null,--审批项目名称
	projectinfo nvarchar(50) not null,--审批项目详情
	pathfile nvarchar(100) not null,--审批项目文件路径
	status int not null--状态（-1删除0正常1禁用）
)
--审批流程管理-审批流程表
create table oa_approvalprocess_process
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	creator bigint not null,--创建者
	addtime datetime not null,--创建时间
	processname nvarchar(20) not null,--流程名称
	processinfo nvarchar(50) not null,--流程详情
	status int not null--状态（-1删除0正常1禁用）
)
--审批流程管理-项目流程表
create table oa_approvalprocess_projectprocess
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	projectprocessname nvarchar(20) not null,--项目流程名称
	projectid bigint not null,--审批项目id
	processid bigint not null,--审批流程id
	status int not null--状态（-1删除0正常1禁用）
)
--审批流程管理-审批人表
create table oa_approvalprocess_approvalpersons
(
	id bigint primary key identity(1,1),
	processid bigint not null,--流程id
	sort int not null,--排序
	status int not null,--状态（-1删除0正常1禁用）
	staffid bigint not null--审批人id
)
--审批流程管理-员工申请表
create table oa_approvalprocess_staffapplys
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	projectprocessid bigint not null,--项目流程id
	staffid bigint not null,--员工id
	applyname nvarchar(20) not null,--申请名称
	remark nvarchar(100) not null,--申请备注
	addtime datetime not null,--申请时间
	status int not null--状态（-1删除0未处理1审批中2审批通过3审批未通过）
)
--审批流程管理-员工申请审批表
create table oa_approvalprocess_staffapplyapprovals
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	staffapplyid bigint not null,--员工申请id
	approvalpersonid bigint not null,--审批人id
	remark nvarchar(4000) not null,--审批备注
	status int not null,--状态（-1删除0未处理1审批未通过2审批通过）
	approvaltime datetime not null,--审批时间
)
--日常工作管理-工作备忘录表
create table oa_runtinework_memorandums
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	staffid bigint not null,--创建者id
	content nvarchar(500) not null,--内容
	addtime datetime not null,--添加时间
	remindtime datetime not null,--提醒时间
	deletetime datetime not null,--删除时间
	status int not null --状态（0显示1隐藏）
)
--绩效管理-制度表
create table oa_achievements_institution
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--公司id
	title nvarchar(50) not null,--制度标题
	content nvarchar(4000) not null,--制度内容
	staffid bigint not null,--制定者id
	addtime datetime not null,--添加时间
	updatetime datetime not null,--修改时间
	impactscore decimal(18,2) not null,--影响分数
	status int not null,--状态（-1删除0整除1禁用）
	sort int not null--排序
)
--绩效管理-绩效明细表
create table oa_achievements_achievementsinfo
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--员工id
	institutionid bigint not null,--制度id
	impactscore decimal(18,2) not null,--影响分数
	impactafterscore decimal(18,2) not null,--影响后分数
	explain nvarchar(50) not null,--绩效说明
	addtime datetime not null--发生时间
)
--公告管理-公告类型表
create table oa_notice_noticetype
(
	id bigint primary key identity(1,1),
	name nvarchar(50) not null,--类型名称
	companyid bigint not null,--公司id
	staffid bigint not null,--创建者id
	addtime datetime not null,--操作时间
	sort int not null,--排序（默认为0,0不进行排序）
	status int not null--状态（-1删除0正常）
)
--公告管理-公告表
create table oa_notice_notice
(
	id bigint primary key identity(1,1),
	typeid bigint not null,--类型id
	companyid bigint not null,--公司id
	title nvarchar(50) not null,--公告标题
	content nvarchar(max) not null,--公告内容
	staffid bigint not null,--创建者id
	sort int not null,--排序（默认为0,0不进行排序）
	clickcount int not null,--点击数
	commentcount int not null,--评论数
	edittime datetime not null,--操作时间
	status int not null --状态（-1删除0正常）
)
--公告管理-公告评论表
create table oa_notice_noticecomment
(
	id bigint primary key identity(1,1),
	noticeid bigint not null,--公告内容id
	companyid bigint not null,--公司id
	comment nvarchar(50) not null,--评论内容
	staffid bigint not null,--评论者id
	addtime datetime not null,--评论时间
	status int not null--状态（-1删除0正常）
)
--论坛管理-版块表
create table oa_forum_section
(
	id bigint primary key identity(1,1),
	sectionname nvarchar(10) not null,--版块名称
	sectioninfo nvarchar(50) not null,--版块详情（板块备注）
	companyid bigint not null,--公司id
	staffid bigint not null,--版主id
	topiccount int not null,--主贴数量
	addtime datetime not null,--创建时间
	status int not null--版块状态
)
--论坛板块-主贴表
create table oa_forum_topic
(
	id bigint primary key identity(1,1),
	sectionid bigint not null,--版块id
	companyid bigint not null,--公司id
	title nvarchar(20) not null,--标题
	content nvarchar(4000) not null,--帖子内容
	staffid bigint not null,--发帖人id
	addtime datetime not null,--发帖时间
	clickcount int not null,--点击数量
	lastclicktime datetime not null,--最后点击时间
	status int not null--帖子状态
)
--论坛管理-跟帖表
create table oa_forum_reply
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--留言人id
	companyid bigint not null,--公司id
	topicid bigint not null,--主贴id
	content nvarchar(max) not null,--跟帖内容
	addtime datetime not null,--跟帖时间
	status int not null--跟帖状态
)
--新增公司的菜单
select * from oa_userpower_pagemenu
insert into oa_userpower_pagemenu(companyid,name,url,image,parentid,remark,status,ispage) select 1 as companyid,name,url,image,parentid,remark,status,ispage from oa_userpower_pagemenu as pm where ispage=1

--员工可以修改自己的信息视图
CREATE VIEW view_oa_member_staffs_staffsinfos
AS
select sf.companyid,sf.departmentid,sf.id,username,email,phone,password,isnull(realname,'') as realname,isnull(sex,-1) as sex,isnull(age,0) as age,isnull(idcard,'') as idcard,isnull(address,'') as address from oa_member_staffs as sf left join oa_member_staffsinfos as si on sf.id=si.staffid

--员工列表信息（常用于下拉列表）
create view view_oa_member_staffsidusername
as
select id,username,companyid from oa_member_staffs

--部门信息+部门人数视图
create view view_oa_member_departmentstaffnumber
as
select id,departmentname,departmentinfo,(select count(0) from oa_member_staffs where departmentid=dt.id) as staffnumber,companyid from oa_member_departments as dt

--员工列表展示信息视图
create view view_oa_member_staffinfolist
as
select sf.id,sf.username,isnull(si.realname,'') as realname,sf.email,sf.phone,isnull((select top 1 positionname from oa_member_staffspositions as sp join oa_member_positions as pt on sp.positionid=pt.id where sp.staffid=sf.id order by level),'') as positionname,isnull(pt.departmentname,'') as departmentname,sf.addtime,sf.status,sf.departmentid,sf.companyid from oa_member_staffs as sf left join oa_member_staffsinfos as si on sf.id=si.staffid left join oa_member_departments as pt on sf.departmentid=pt.id

--职位列表展示信息视图
create view view_oa_member_position
as
select id,positionname,positioninfo,(select positionname from oa_member_positions where id=pt.parentid) as parentpositionname,(select departmentname from oa_member_departments where id=pt.departmentid) as departmentname,level,departmentid,companyid from oa_member_positions pt

--员工职位列表展示信息视图
create view view_oa_member_staffposition
as
select sp.id,sp.staffid,sp.positionid,sf.username,pt.positionname,sp.companyid,pt.level from oa_member_staffspositions as sp left join oa_member_staffs sf on sp.staffid=sf.id left join oa_member_positions as pt on sp.positionid=pt.id

--职位功能列表视图
create view view_oa_member_positionfunctionlist
as
select pf.id,pf.companyid,pf.positionid,pf.functionid,pf.status,pt.positionname,pt.positioninfo,ft.functionname,ft.functioninfo,ft.parentid from oa_userpower_positionsfunctions as pf join oa_member_positions as pt on pf.positionid=pt.id join oa_userpower_functions as ft on pf.functionid=ft.id

--功能ACTION列表视图
create view view_oa_userpower_functionactionlist
as
select fa.id,fa.companyid,fa.functionid,fa.actionid,fa.status,at.actionname,at.actioninfo from oa_userpower_functionsactions as fa join oa_userpower_functions as ft on fa.functionid=ft.id join oa_userpower_actions as at on fa.actionid=at.id

--项目人员列表显示信息视图
create view view_oa_project_projectstafflist
as
select ps.id,ps.rolename,sf.username,si.realname,sf.email,sf.phone,sf.companyid,ps.staffid,ps.projectid,ps.status from oa_project_projectsstaffs as ps join oa_member_staffs as sf on staffid = sf.id join oa_member_staffsinfos as si on sf.id=si.staffid

--项目bug列表显示信息视图
create view view_oa_project_projectbuglist
as
select pb.id,pb.projectid,pj.projectname,pj.companyid,pb.title,pb.type,pb.degree,pb.writer,(select username from oa_member_staffs where id=pb.writer) as writerusername,pb.editor,(select username from oa_member_staffs where id=pb.editor) as editorusername,pb.status from oa_project_projectsbugs as pb join oa_project_projects as pj on pb.projectid=pj.id

--项目bug显示信息视图
create view view_oa_project_projectbug
as
select pb.id,pj.projectname,pb.title,pb.type,pb.degree,pb.reproducestep,pb.dostep,pb.writer,(select username from oa_member_staffs where id=pb.writer) as writerusername,pb.editor,(select username from oa_member_staffs where id=pb.editor) as editorusername,pb.status,pb.addtime,pb.confirmtime,pb.solvetime,pb.finishtime,pb.projectid,pj.companyid from oa_project_projectsbugs as pb join oa_project_projects as pj on pb.projectid=pj.id

--审批流程项目列表展示信息视图
create view view_oa_approvalprocess_projectprocess
as
select pp.id,pp.projectprocessname,pp.projectid,pp.processid,pj.projectname,pj.projectinfo,pj.pathfile,pc.processname,pc.processinfo,pp.companyid,pp.status from oa_approvalprocess_projectprocess as pp left join oa_approvalprocess_projects as pj on pp.projectid=pj.id left join oa_approvalprocess_process as pc on pp.processid=pc.id

--审批流程审批人列表展示信息视图
create view view_oa_approvalprocess_processperson
as
select ap.id,ap.processid,ap.staffid,ap.sort,ap.status,pc.processname,pc.companyid,sf.username from oa_approvalprocess_approvalpersons as ap join oa_approvalprocess_process as pc on ap.processid=pc.id join oa_member_staffs as sf on ap.staffid=sf.id

--审批流程员工申请列表展示信息视图
create view view_oa_approvalprocess_staffapplylist
as
select sa.id,sa.companyid,sa.projectprocessid,sa.staffid,sa.applyname,sa.remark,sa.addtime,sa.status,pp.projectid,pp.processid,pj.projectname,pc.processname,sf.username,isnull((select top 1 sfs.username from oa_approvalprocess_staffapplyapprovals as sap join oa_approvalprocess_approvalpersons as ap on sap.approvalpersonid=ap.id join oa_member_staffs as sfs on ap.staffid=sfs.id where staffapplyid=sa.id and sap.status!=0 order by sap.approvaltime desc,sap.id asc),'无') as approvalusername,isnull((select top 1 sap.approvaltime from oa_approvalprocess_staffapplyapprovals as sap join oa_approvalprocess_approvalpersons as ap on sap.approvalpersonid=ap.id join oa_member_staffs as sfs on ap.staffid=sfs.id where staffapplyid=sa.id and sap.status!=0 order by sap.approvaltime desc,sap.id asc),'1970-01-01 08:00:00') as approvaltime,isnull((select top 1 sap.status from oa_approvalprocess_staffapplyapprovals as sap join oa_approvalprocess_approvalpersons as ap on sap.approvalpersonid=ap.id join oa_member_staffs as sfs on ap.staffid=sfs.id where sap.staffapplyid=sa.id and sap.status!=0 order by sap.approvaltime desc,sap.id asc),0) as approvalstatus,isnull((select top 1 ap.staffid from oa_approvalprocess_staffapplyapprovals as sap join oa_approvalprocess_approvalpersons as ap on sap.approvalpersonid=ap.id join oa_member_staffs as sfs on ap.staffid=sfs.id where sap.staffapplyid=sa.id and sap.status!=0 order by sap.approvaltime desc,sap.id asc),0) as approvalstaffid from oa_approvalprocess_staffapplys as sa join oa_approvalprocess_projectprocess as pp on sa.projectprocessid=pp.id join oa_approvalprocess_projects as pj on pp.projectid=pj.id join oa_approvalprocess_process as pc on pp.processid=pc.id join oa_member_staffs as sf on sa.staffid=sf.id

--审批流程员工申请审批列表展示信息视图
create view view_oa_approvalprocess_staffapplyapprovallist
as
select saa.id,saa.companyid,saa.staffapplyid,saa.approvaltime,ap.sort,saa.status,ap.staffid,sf.username from oa_approvalprocess_staffapplyapprovals as saa join oa_approvalprocess_approvalpersons as ap on saa.approvalpersonid=ap.id join oa_member_staffs as sf on ap.staffid=sf.id

--审批流程员工申请关联查询
create view view_oa_approvalprocess_staffapplyall
as
select sa.id,sa.applyname,pp.projectid,pp.processid,sa.companyid,sa.projectprocessid,sa.staffid,ap.staffid as approvalstaffid,saa.approvalpersonid,ap.sort from oa_approvalprocess_staffapplys as sa join oa_approvalprocess_projectprocess as pp on sa.projectprocessid=pp.id join oa_approvalprocess_staffapplyapprovals as saa on sa.id=saa.staffapplyid join oa_approvalprocess_approvalpersons as ap on saa.approvalpersonid=ap.id

--绩效明细列表
create view view_oa_achievements_achievementinfolist
as
select ai.id,ai.staffid,ai.institutionid,sf.username,isnull(si.realname,'未填写') as realname,isnull(it.title,ai.explain) as title,ai.impactscore,ai.impactafterscore,ai.addtime,sf.companyid,sf.departmentid from oa_achievements_achievementsinfo as ai left join oa_achievements_institution as it on ai.institutionid=it.id left join oa_member_staffs as sf on ai.staffid=sf.id left join oa_member_staffsinfos as si on ai.staffid=si.staffid where sf.isachievement=1

--员工绩效列表
create view view_oa_achievements_staffachievementlist
as
select sf.id,sf.username,isnull(si.realname,'未填写') as realname,dt.departmentname,sf.departmentid,sf.companyid,isnull((select top 1 impactafterscore from oa_achievements_achievementsinfo where staffid=sf.id order by addtime desc),100.00) as score from oa_member_staffs as sf left join oa_member_staffsinfos as si on sf.id=si.staffid left join oa_member_departments as dt on sf.departmentid=dt.id where sf.isachievement=1

--论坛版块列表
create view view_oa_forum_section
as
select se.id,se.sectionname,se.sectioninfo,se.companyid,se.staffid,sf.username,se.topiccount,se.addtime,se.status from oa_forum_section as se join oa_member_staffs as sf on se.staffid=sf.id

--论坛主贴列表
create view view_oa_forum_topic
as
select tp.id,tp.companyid,tp.sectionid,tp.title,tp.content,tp.staffid,sf.username,tp.addtime,tp.clickcount,tp.lastclicktime,tp.status from oa_forum_topic as tp join oa_member_staffs as sf on tp.staffid=sf.id

--论坛跟帖列表
create view view_oa_forum_reply
as
select rp.id,rp.companyid,rp.staffid,sf.username,rp.topicid,tp.sectionid,rp.content,rp.addtime,rp.status from oa_forum_reply as rp join oa_forum_topic as tp on rp.topicid=tp.id join oa_member_staffs as sf on rp.staffid=sf.id

--公告列表
create view view_oa_notice_notice
as
select nt.id,nt.typeid,nt.companyid,nt.title,nt.content,nt.staffid,sf.username,nt.sort,nt.edittime,nt.clickcount,nt.commentcount,nt.status from oa_notice_notice as nt join oa_member_staffs as sf on nt.staffid=sf.id

--公告评论列表
create view view_oa_notice_noticecomment
as
select nc.id,nc.noticeid,nc.companyid,nc.comment,nc.staffid,sf.username,nc.addtime,nc.status from oa_notice_noticecomment as nc join oa_member_staffs as sf on nc.staffid=sf.id

--后台公司列表
create view view_oa_member_companylist
as
select cp.id,cp.companyname,cp.companyinfo,cp.status,cp.addtime,(select count(0) from oa_member_departments where companyid=cp.id) as departmentcount,(select count(0) from oa_member_staffs where companyid=cp.id) as staffcount from oa_member_companys as cp
--后台权限日志
create view view_oa_power_powerloglist
as
select pl.id,pl.tablename,pl.type,pl.content,pl.auserid,au.username,pl.addip,pl.addtime from oa_power_powerlogs as pl join oa_power_ausers as au on pl.auserid=au.id
--后台管理员操作日志
create view view_oa_power_dologlist
as
select dl.id,dl.auserid,au.username,dl.type,dl.addip,dl.addtime,dl.doinfo from oa_power_dologs as dl left join oa_power_ausers as au on dl.auserid=au.id
--后台管理员列表
create view view_oa_power_auserlist
as
select au.id,au.username,au.password,au.userword,au.roleid,isnull(rl.rolename,'默认权限') as rolename,au.status,au.addtime,isnull(ai.realname,'') as realname,isnull(ai.qq,'') as qq,isnull(ai.email,'') as email,isnull(ai.phone,'') as phone from oa_power_ausers as au left join oa_power_ausersinfos as ai on au.id=ai.auserid left join oa_power_roles as rl on au.roleid=rl.id

--后台角色功能列表
create view view_oa_power_rolefunctionlist
as
select rf.id,rf.roleid,rf.functionid,rf.status,rl.rolename,rl.roleinfo,fu.functionname,fu.functioninfo,fu.parentid from oa_power_rolesfunctions as rf left join oa_power_roles as rl on rf.roleid=rl.id left join oa_power_functions as fu on rf.functionid=fu.id

--后台功能action列表
create view view_oa_power_functionactionlist
as
select fa.id,fa.actionid,fa.functionid,fa.status,at.actionname,at.actioninfo from oa_power_functionsactions as fa join oa_power_actions as at on fa.actionid=at.id

----------------------存储过程分割线------------------------

if(object_id('proc_updateauser','P') is not null)
	drop proc proc_updateauser
go
create proc proc_updateauser
	@id bigint,
	@roleid bigint,
	@username nvarchar(20),
	@password nvarchar(30),
	@userword nvarchar(30),
	@realname nvarchar(10),
	@qq varchar(20),
	@email varchar(50),
	@phone varchar(20),
	@result int output,
	@msg nvarchar(20) output
as
begin
	select * from oa_power_ausers where id=@id
	declare @tran_error int;
	if(@@rowcount=1)--修改
	begin
		begin tran Tran_auser --开始事务
		set @tran_error=0;
		begin TRY
			if(@password!='')
			begin
				update oa_power_ausers set password=@password where id=@id
				set @tran_error = @tran_error+@@ERROR
			end
			if(@userword!='')
			begin
				update oa_power_ausers set userword=@userword where id=@id
				set @tran_error = @tran_error+@@ERROR
			end
			select * from oa_power_ausers where username=@username and id!=@id
			if(@@rowcount>0)--判断数据是否存在
			begin
				set @msg='用户名已存在！';
				set @tran_error = @tran_error+1
			end
			else
			begin
				update oa_power_ausers set username=@username,roleid=@roleid where id=@id
				set @tran_error = @tran_error+@@ERROR
				select * from oa_power_ausersinfos where auserid=@id
				if(@@rowcount=1)--判断数据是否存在
				begin
					update oa_power_ausersinfos set realname=@realname,qq=@qq,email=@email,phone=@phone where auserid=@id
					set @tran_error = @tran_error+@@ERROR
				end
				else--如果数据不存在，则新增
				begin
					insert into oa_power_ausersinfos(auserid,realname,qq,email,phone) values(@id,@realname,@qq,@email,@phone)
					set @tran_error = @tran_error+@@ERROR
				end
			end
		end TRY
		begin CATCH
			set @tran_error = @tran_error+@@ERROR
		end CATCH
		if(@tran_error>0)
		begin
			set @result = 0;
			set @msg=@msg+'修改失败';
			if(charindex(@msg,'用户名已存在')>=0)
			begin
				--提交事务
				commit tran Tran_auser;
			end
			else
			begin
				--执行失败，回滚事务
				rollback tran Tran_auser;
			end
		end
		else
		begin
			set @result = 1;
			set @msg='修改成功';
			--执行成功,提交事务
			commit tran Tran_auser;
		end
	end
	else--新增
	begin
		begin tran Tran_auser --开始事务
		set @tran_error=0;
		begin TRY
			select * from oa_power_ausers where username=@username
			if(@@rowcount>1)--判断数据是否存在
			begin
				set @msg='用户名已存在！';
				set @tran_error = @tran_error+1
			end
			else
			begin
				insert into oa_power_ausers(username,password,userword,roleid,status,addtime) values(@username,@password,@userword,@roleid,0,getdate())
				set @tran_error = @tran_error+@@ERROR
				set @id = SCOPE_IDENTITY();
				insert into oa_power_ausersinfos(auserid,realname,qq,email,phone) values(@id,@realname,@qq,@email,@phone)
				set @tran_error = @tran_error+@@ERROR
			end
		end TRY
		begin CATCH
			set @tran_error = @tran_error+@@ERROR
		end CATCH
		if(@tran_error>0)
		begin
			set @result = 0;
			set @msg=@msg+'新增失败';
			if(charindex(@msg,'用户名已存在')>=0)
			begin
				--提交事务
				commit tran Tran_auser;
			end
			else
			begin
				--执行失败，回滚事务
				rollback tran Tran_auser;
			end
		end
		else
		begin
			set @result = 1;
			set @msg='新增成功';
			--执行成功,提交事务
			commit tran Tran_auser;
		end
	end
end
go

CREATE PROCEDURE [dbo].[GetNameByCustomerId]
 @CustomerId varchar(5),
 @ContactName varchar(30) output
AS
BEGIN
 set @ContactName=@CustomerId;
END
GO

--测试
declare @result int,@msg nvarchar(20)
set @result=-1
set @msg=''

exec proc_updateauser 10,0,'hai','','','蒋海东1','971368174','971368174@qq.com','13452579221',@result output,@msg output
print @result
print @msg
----------------------存储过程分割线------------------------

