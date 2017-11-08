if db_id('oadb') is null
create database oadb
go
use oadb
--Ȩ�޹���-����Ա��
create table oa_power_ausers
(
	id bigint primary key identity(1,1),
	username nvarchar(20) unique not null,--�û���
	[password] nvarchar(30) not null,--����
	userword nvarchar(30) not null,--����
	roleid bigint not null,--��ɫid
	[status] int not null,--״̬
	addtime datetime not null--���ʱ��
)
--Ȩ�޹���-����Ա�����
create table oa_power_ausersinfos
(
	id bigint primary key identity(1,1),
	auserid bigint not null,--����Աid
	realname nvarchar(10) not null,--��ʵ����
	qq varchar(20) not null,--QQ����
	email varchar(50) not null,--����
	phone varchar(20) not null,--��ϵ�绰
)
--Ȩ�޹���-�˵���
create table oa_power_pagemenu
(
	id bigint primary key identity(1,1),
	name nvarchar(10) not null,--�˵�����
	url nvarchar(100) not null,--�˵�·��
	image nvarchar(100) not null,--�˵�ͼƬ
	parentid bigint not null,--����id
	remark nvarchar(50) not null,--��ע
	status int not null,--״̬��0Ϊ������1Ϊ���ã�
	ispage int not null--�Ƿ�Ϊҳ�棨0��Ϊ��1Ϊ��
)
--Ȩ�޹���-��ɫ��
create table oa_power_roles
(
	id bigint primary key identity(1,1),
	rolename nvarchar(20) not null,--��ɫ����
	roleinfo nvarchar(50) not null--��ɫ����
)
--Ȩ�޹���-���ܱ�
create table oa_power_functions
(
	id bigint primary key identity(1,1),
	functionname nvarchar(20) not null,--��������
	functioninfo nvarchar(50) not null,--��������
	parentid bigint not null--����id
)
--Ȩ�޹���-��ɫ���ܱ�
create table oa_power_rolesfunctions
(
	id bigint primary key identity(1,1),
	roleid bigint not null,--��ɫid
	functionid bigint not null,--����id
	status int not null --״̬��0Ϊδѡ��1Ϊ��ѡ��
)
--Ȩ�޹���-Action��
create table oa_power_actions
(
	id bigint primary key identity(1,1),
	actionname nvarchar(100) not null,--action����
	actioninfo nvarchar(50) not null--action����
)
--Ȩ�޹���-����Action��
create table oa_power_functionsactions
(
	id bigint primary key identity(1,1),
	functionid bigint not null,--����id
	actionid bigint not null,--actionid
	status int not null--״̬��0δѡ�У�1��ѡ�У�
)
--Ȩ�޹���-Ȩ����־��
create table oa_power_powerlogs
(
	id bigint primary key identity(1,1),
	tablename nvarchar(50) not null,--����
	type int not null,--�������ͣ�1��2ɾ3��
	content nvarchar(max) not null,--����
	auserid bigint not null,--����Աid
	addip nvarchar(20) not null,--������ip
	addtime datetime not null--����ʱ��
)
--Ȩ�޹���-������־��
create table oa_power_dologs
(
	id bigint primary key identity(1,1),
	auserid bigint not null,--����Աid
	type nvarchar(10) not null,--����
	addip nvarchar(20) not null,--������ip
	addtime datetime not null,--����ʱ��
	doinfo nvarchar(50) not null--��������
)
--Ȩ�޹���-����Ա��¼��
create table oa_power_auserslogins
(
	id bigint primary key identity(1,1),
	auserid bigint not null,--����Աid
	addip nvarchar(20) not null,--��½ip
	addtime datetime not null,--���ʱ��
	dotime datetime not null,--����ʱ��
	cookie nvarchar(100) not null,--cookie�ַ���
	expiretime int not null --����ʱ�䣨��λ�����ӣ�
)
--ǰ̨Ȩ�޹���-�˵���
create table oa_userpower_pagemenu
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	name nvarchar(10) not null,--�˵�����
	url nvarchar(100) not null,--�˵�·��
	image nvarchar(100) not null,--�˵�ͼƬ
	parentid bigint not null,--����id
	remark nvarchar(50) not null,--��ע
	status int not null,--״̬��-1Ϊɾ����0Ϊ������1Ϊ���ã�
	ispage int not null--�Ƿ�Ϊҳ�棨0��Ϊ��1Ϊ��
)
--ǰ̨Ȩ�޹���-���ܱ�
create table oa_userpower_functions
(
	id bigint primary key identity(1,1),
	functionname nvarchar(20) not null,--��������
	functioninfo nvarchar(50) not null,--��������
	parentid bigint not null--����id
)
--ǰ̨Ȩ�޹���-ְλ���ܱ�
create table oa_userpower_positionsfunctions
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	positionid bigint not null,--ְλid
	functionid bigint not null,--����id
	status int not null--״̬��-1δ��Ȩ��0δѡ�У�1��ѡ�У�
)
--ǰ̨Ȩ�޹���-Action��
create table oa_userpower_actions
(
	id bigint primary key identity(1,1),
	actionname nvarchar(20) not null,--action����
	actioninfo nvarchar(50) not null--Action����
)
--ǰ̨Ȩ�޹���-����Action��
create table oa_userpower_functionsactions
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	functionid bigint not null,--����id
	actionid bigint not null,--actionid
	status int not null--״̬��-1δ��Ȩ��0δѡ�У�1��ѡ�У�
)
--ǰ̨Ȩ�޹���-Ȩ����־��
create table oa_userpower_powerlogs
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	tablename nvarchar(50) not null,--����
	type int not null,--��������
	content nvarchar(100) not null,--����
	staffid bigint not null,--Ա��id
	staffname nvarchar(20) not null,--�û���
	addtime datetime not null --����ʱ��
)
--��Ա����-��˾��
create table oa_member_companys
(
	id bigint primary key identity(1,1),
	companyname nvarchar(50) not null,--��˾����
	companyinfo nvarchar(100) not null,--��˾����
	status int not null,--״̬
	addtime datetime not null--����ʱ��
)
--��Ա����-���ű�
create table oa_member_departments
(
	id bigint primary key identity(1,1),
	departmentname nvarchar(50) not null,--��������
	departmentinfo nvarchar(100) not null,--��������
	companyid bigint not null,--��˾id
	status int not null,--״̬
	addtime datetime not null--����ʱ��
)
--��Ա����-Ա����
create table oa_member_staffs
(
	id bigint primary key identity(1,1),
	username nvarchar(20) not null unique,--�û���
	email nvarchar(50) not null unique,--����
	phone varchar(20) not null unique,--�ֻ�
	password nvarchar(30) not null,--����
	userword nvarchar(20) not null,--����
	status int not null,--״̬
	departmentid bigint not null,--����id
	companyid bigint not null,--��˾id
	isall int not null,--�Ƿ�鿴ȫ����0Ϊ���ܣ�1Ϊ�ܣ�
	isachievement int not null,--�Ƿ�Ч���ˣ�0Ϊ�ޣ�1Ϊ�У�
	addtime datetime not null--���ʱ��
)
--��Ա����-Ա�������
create table oa_member_staffsinfos
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--Ա��id
	realname nvarchar(10) not null,--��ʵ����
	sex int not null,--�Ա�0Ů1��
	age int not null,--����
	idcard varchar(20) not null,--���֤����
	address nvarchar(100) not null--��ס��ַ
)
--��Ա����-������־��
create table oa_member_dologs
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--Ա��id
	type nvarchar(10) not null,--����
	addip nvarchar(20) not null,--����ip
	addtime datetime not null,--����ʱ��
	doinfo nvarchar(50) not null--��������
)
--��Ա����-Ա����¼��
create table oa_member_staffslogins
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--Ա��id
	addip varchar(20) not null,--��½ip
	addtime datetime not null,--��½ʱ��
	dotime datetime not null,--����ʱ��
	cookie varchar(100) not null,--cookie�ַ���
	expiretime int not null --����ʱ�䵥λ������
)
--��Ա����-ְλ��
create table oa_member_positions
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	departmentid bigint not null,--����id��--2016/5/18����
	positionname nvarchar(10) not null,--ְλ����
	positioninfo nvarchar(50) not null,--ְλ����
	parentid bigint not null,--����id��--2016/5/18����
	level int not null, --����ȼ���1���2��֮��--2016/5/18����
	status int not null,--״̬
	sort int not null--����
)
--��Ա����-Ա��ְλ��
create table oa_member_staffspositions
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	staffid bigint not null,--Ա��id
	positionid bigint not null,--ְλid
)
--��Ŀ����-��Ŀ��
create table oa_project_projects
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	staffid bigint not null,--������id
	person_responsibleid bigint not null,--������id
	projectname nvarchar(20) not null,--��Ŀ����
	projectinfo nvarchar(4000) not null,--��Ŀ����
	progress decimal not null,--����
	status int not null,--״̬��0�½�1������2����ɣ�
	addtime datetime not null,--���ʱ��
	expectbegintime datetime not null,--Ԥ�ƿ�ʼʱ��
	realitybegintime datetime not null,--ʵ�ʿ�ʼʱ��
	updatetime datetime not null,--����ʱ��
	expectfinishtime datetime not null,--Ԥ�����ʱ��
	realityfinishtime datetime not null--ʵ�����ʱ��
)
--��Ŀ����-��Ŀ��Ա��
create table oa_project_projectsstaffs
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	projectid bigint not null,--��Ŀid
	staffid bigint not null,--��Աid
	rolename nvarchar(10) not null,--��Ŀ��ɫ����
	status int not null--״̬��-1ɾ����0������
)
--��Ŀ����-��ĿBug��
create table oa_project_projectsbugs
(
	id bigint primary key identity(1,1),
	projectid bigint not null,--��Ŀid
	title nvarchar(50) not null,--bug����
	type int not null,--bug����(������� = 0, �����Ż� = 1, ���ȱ�� = 2, ������� = 3, ��װ���� = 4, ��ȫ��� = 5, �������� = 6, ��׼�淶 = 7, ���Խű� = 8, ���� = 9)
	degree int not null,--���ȳ̶ȣ�0��ͨ1��Ҫ2������Ĭ��0��
	reproducestep nvarchar(4000) not null,--���ֲ���
	dostep nvarchar(4000) not null,--��������
	writer bigint not null,--�����
	editor bigint not null,--�޸���
	status int not null,--״̬��0δȷ��1δ���2���ر�3����ɣ�
	addtime datetime not null,--���ʱ��
	confirmtime datetime not null,--ȷ��ʱ��
	solvetime datetime not null,--���ʱ��
	closetime datetime not null--�ر�ʱ��
)
--�������̹���-������Ŀ��
create table oa_approvalprocess_projects
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	creator	bigint not null,--�����ߣ���ӦԱ����id
	addtime datetime not null,--����ʱ��
	projectname nvarchar(20) not null,--������Ŀ����
	projectinfo nvarchar(50) not null,--������Ŀ����
	pathfile nvarchar(100) not null,--������Ŀ�ļ�·��
	status int not null--״̬��-1ɾ��0����1���ã�
)
--�������̹���-�������̱�
create table oa_approvalprocess_process
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	creator bigint not null,--������
	addtime datetime not null,--����ʱ��
	processname nvarchar(20) not null,--��������
	processinfo nvarchar(50) not null,--��������
	status int not null--״̬��-1ɾ��0����1���ã�
)
--�������̹���-��Ŀ���̱�
create table oa_approvalprocess_projectprocess
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	projectprocessname nvarchar(20) not null,--��Ŀ��������
	projectid bigint not null,--������Ŀid
	processid bigint not null,--��������id
	status int not null--״̬��-1ɾ��0����1���ã�
)
--�������̹���-�����˱�
create table oa_approvalprocess_approvalpersons
(
	id bigint primary key identity(1,1),
	processid bigint not null,--����id
	sort int not null,--����
	status int not null,--״̬��-1ɾ��0����1���ã�
	staffid bigint not null--������id
)
--�������̹���-Ա�������
create table oa_approvalprocess_staffapplys
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	projectprocessid bigint not null,--��Ŀ����id
	staffid bigint not null,--Ա��id
	applyname nvarchar(20) not null,--��������
	remark nvarchar(100) not null,--���뱸ע
	addtime datetime not null,--����ʱ��
	status int not null--״̬��-1ɾ��0δ����1������2����ͨ��3����δͨ����
)
--�������̹���-Ա������������
create table oa_approvalprocess_staffapplyapprovals
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	staffapplyid bigint not null,--Ա������id
	approvalpersonid bigint not null,--������id
	remark nvarchar(4000) not null,--������ע
	status int not null,--״̬��-1ɾ��0δ����1����δͨ��2����ͨ����
	approvaltime datetime not null,--����ʱ��
)
--�ճ���������-��������¼��
create table oa_runtinework_memorandums
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	staffid bigint not null,--������id
	content nvarchar(500) not null,--����
	addtime datetime not null,--���ʱ��
	remindtime datetime not null,--����ʱ��
	deletetime datetime not null,--ɾ��ʱ��
	status int not null --״̬��0��ʾ1���أ�
)
--��Ч����-�ƶȱ�
create table oa_achievements_institution
(
	id bigint primary key identity(1,1),
	companyid bigint not null,--��˾id
	title nvarchar(50) not null,--�ƶȱ���
	content nvarchar(4000) not null,--�ƶ�����
	staffid bigint not null,--�ƶ���id
	addtime datetime not null,--���ʱ��
	updatetime datetime not null,--�޸�ʱ��
	impactscore decimal(18,2) not null,--Ӱ�����
	status int not null,--״̬��-1ɾ��0����1���ã�
	sort int not null--����
)
--��Ч����-��Ч��ϸ��
create table oa_achievements_achievementsinfo
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--Ա��id
	institutionid bigint not null,--�ƶ�id
	impactscore decimal(18,2) not null,--Ӱ�����
	impactafterscore decimal(18,2) not null,--Ӱ������
	explain nvarchar(50) not null,--��Ч˵��
	addtime datetime not null--����ʱ��
)
--�������-�������ͱ�
create table oa_notice_noticetype
(
	id bigint primary key identity(1,1),
	name nvarchar(50) not null,--��������
	companyid bigint not null,--��˾id
	staffid bigint not null,--������id
	addtime datetime not null,--����ʱ��
	sort int not null,--����Ĭ��Ϊ0,0����������
	status int not null--״̬��-1ɾ��0������
)
--�������-�����
create table oa_notice_notice
(
	id bigint primary key identity(1,1),
	typeid bigint not null,--����id
	companyid bigint not null,--��˾id
	title nvarchar(50) not null,--�������
	content nvarchar(max) not null,--��������
	staffid bigint not null,--������id
	sort int not null,--����Ĭ��Ϊ0,0����������
	clickcount int not null,--�����
	commentcount int not null,--������
	edittime datetime not null,--����ʱ��
	status int not null --״̬��-1ɾ��0������
)
--�������-�������۱�
create table oa_notice_noticecomment
(
	id bigint primary key identity(1,1),
	noticeid bigint not null,--��������id
	companyid bigint not null,--��˾id
	comment nvarchar(50) not null,--��������
	staffid bigint not null,--������id
	addtime datetime not null,--����ʱ��
	status int not null--״̬��-1ɾ��0������
)
--��̳����-����
create table oa_forum_section
(
	id bigint primary key identity(1,1),
	sectionname nvarchar(10) not null,--�������
	sectioninfo nvarchar(50) not null,--������飨��鱸ע��
	companyid bigint not null,--��˾id
	staffid bigint not null,--����id
	topiccount int not null,--��������
	addtime datetime not null,--����ʱ��
	status int not null--���״̬
)
--��̳���-������
create table oa_forum_topic
(
	id bigint primary key identity(1,1),
	sectionid bigint not null,--���id
	companyid bigint not null,--��˾id
	title nvarchar(20) not null,--����
	content nvarchar(4000) not null,--��������
	staffid bigint not null,--������id
	addtime datetime not null,--����ʱ��
	clickcount int not null,--�������
	lastclicktime datetime not null,--�����ʱ��
	status int not null--����״̬
)
--��̳����-������
create table oa_forum_reply
(
	id bigint primary key identity(1,1),
	staffid bigint not null,--������id
	companyid bigint not null,--��˾id
	topicid bigint not null,--����id
	content nvarchar(max) not null,--��������
	addtime datetime not null,--����ʱ��
	status int not null--����״̬
)
--������˾�Ĳ˵�
select * from oa_userpower_pagemenu
insert into oa_userpower_pagemenu(companyid,name,url,image,parentid,remark,status,ispage) select 1 as companyid,name,url,image,parentid,remark,status,ispage from oa_userpower_pagemenu as pm where ispage=1

--Ա�������޸��Լ�����Ϣ��ͼ
CREATE VIEW view_oa_member_staffs_staffsinfos
AS
select sf.companyid,sf.departmentid,sf.id,username,email,phone,password,isnull(realname,'') as realname,isnull(sex,-1) as sex,isnull(age,0) as age,isnull(idcard,'') as idcard,isnull(address,'') as address from oa_member_staffs as sf left join oa_member_staffsinfos as si on sf.id=si.staffid

--Ա���б���Ϣ�������������б�
create view view_oa_member_staffsidusername
as
select id,username,companyid from oa_member_staffs

--������Ϣ+����������ͼ
create view view_oa_member_departmentstaffnumber
as
select id,departmentname,departmentinfo,(select count(0) from oa_member_staffs where departmentid=dt.id) as staffnumber,companyid from oa_member_departments as dt

--Ա���б�չʾ��Ϣ��ͼ
create view view_oa_member_staffinfolist
as
select sf.id,sf.username,isnull(si.realname,'') as realname,sf.email,sf.phone,isnull((select top 1 positionname from oa_member_staffspositions as sp join oa_member_positions as pt on sp.positionid=pt.id where sp.staffid=sf.id order by level),'') as positionname,isnull(pt.departmentname,'') as departmentname,sf.addtime,sf.status,sf.departmentid,sf.companyid from oa_member_staffs as sf left join oa_member_staffsinfos as si on sf.id=si.staffid left join oa_member_departments as pt on sf.departmentid=pt.id

--ְλ�б�չʾ��Ϣ��ͼ
create view view_oa_member_position
as
select id,positionname,positioninfo,(select positionname from oa_member_positions where id=pt.parentid) as parentpositionname,(select departmentname from oa_member_departments where id=pt.departmentid) as departmentname,level,departmentid,companyid from oa_member_positions pt

--Ա��ְλ�б�չʾ��Ϣ��ͼ
create view view_oa_member_staffposition
as
select sp.id,sp.staffid,sp.positionid,sf.username,pt.positionname,sp.companyid,pt.level from oa_member_staffspositions as sp left join oa_member_staffs sf on sp.staffid=sf.id left join oa_member_positions as pt on sp.positionid=pt.id

--ְλ�����б���ͼ
create view view_oa_member_positionfunctionlist
as
select pf.id,pf.companyid,pf.positionid,pf.functionid,pf.status,pt.positionname,pt.positioninfo,ft.functionname,ft.functioninfo,ft.parentid from oa_userpower_positionsfunctions as pf join oa_member_positions as pt on pf.positionid=pt.id join oa_userpower_functions as ft on pf.functionid=ft.id

--����ACTION�б���ͼ
create view view_oa_userpower_functionactionlist
as
select fa.id,fa.companyid,fa.functionid,fa.actionid,fa.status,at.actionname,at.actioninfo from oa_userpower_functionsactions as fa join oa_userpower_functions as ft on fa.functionid=ft.id join oa_userpower_actions as at on fa.actionid=at.id

--��Ŀ��Ա�б���ʾ��Ϣ��ͼ
create view view_oa_project_projectstafflist
as
select ps.id,ps.rolename,sf.username,si.realname,sf.email,sf.phone,sf.companyid,ps.staffid,ps.projectid,ps.status from oa_project_projectsstaffs as ps join oa_member_staffs as sf on staffid = sf.id join oa_member_staffsinfos as si on sf.id=si.staffid

--��Ŀbug�б���ʾ��Ϣ��ͼ
create view view_oa_project_projectbuglist
as
select pb.id,pb.projectid,pj.projectname,pj.companyid,pb.title,pb.type,pb.degree,pb.writer,(select username from oa_member_staffs where id=pb.writer) as writerusername,pb.editor,(select username from oa_member_staffs where id=pb.editor) as editorusername,pb.status from oa_project_projectsbugs as pb join oa_project_projects as pj on pb.projectid=pj.id

--��Ŀbug��ʾ��Ϣ��ͼ
create view view_oa_project_projectbug
as
select pb.id,pj.projectname,pb.title,pb.type,pb.degree,pb.reproducestep,pb.dostep,pb.writer,(select username from oa_member_staffs where id=pb.writer) as writerusername,pb.editor,(select username from oa_member_staffs where id=pb.editor) as editorusername,pb.status,pb.addtime,pb.confirmtime,pb.solvetime,pb.finishtime,pb.projectid,pj.companyid from oa_project_projectsbugs as pb join oa_project_projects as pj on pb.projectid=pj.id

--����������Ŀ�б�չʾ��Ϣ��ͼ
create view view_oa_approvalprocess_projectprocess
as
select pp.id,pp.projectprocessname,pp.projectid,pp.processid,pj.projectname,pj.projectinfo,pj.pathfile,pc.processname,pc.processinfo,pp.companyid,pp.status from oa_approvalprocess_projectprocess as pp left join oa_approvalprocess_projects as pj on pp.projectid=pj.id left join oa_approvalprocess_process as pc on pp.processid=pc.id

--���������������б�չʾ��Ϣ��ͼ
create view view_oa_approvalprocess_processperson
as
select ap.id,ap.processid,ap.staffid,ap.sort,ap.status,pc.processname,pc.companyid,sf.username from oa_approvalprocess_approvalpersons as ap join oa_approvalprocess_process as pc on ap.processid=pc.id join oa_member_staffs as sf on ap.staffid=sf.id

--��������Ա�������б�չʾ��Ϣ��ͼ
create view view_oa_approvalprocess_staffapplylist
as
select sa.id,sa.companyid,sa.projectprocessid,sa.staffid,sa.applyname,sa.remark,sa.addtime,sa.status,pp.projectid,pp.processid,pj.projectname,pc.processname,sf.username,isnull((select top 1 sfs.username from oa_approvalprocess_staffapplyapprovals as sap join oa_approvalprocess_approvalpersons as ap on sap.approvalpersonid=ap.id join oa_member_staffs as sfs on ap.staffid=sfs.id where staffapplyid=sa.id and sap.status!=0 order by sap.approvaltime desc,sap.id asc),'��') as approvalusername,isnull((select top 1 sap.approvaltime from oa_approvalprocess_staffapplyapprovals as sap join oa_approvalprocess_approvalpersons as ap on sap.approvalpersonid=ap.id join oa_member_staffs as sfs on ap.staffid=sfs.id where staffapplyid=sa.id and sap.status!=0 order by sap.approvaltime desc,sap.id asc),'1970-01-01 08:00:00') as approvaltime,isnull((select top 1 sap.status from oa_approvalprocess_staffapplyapprovals as sap join oa_approvalprocess_approvalpersons as ap on sap.approvalpersonid=ap.id join oa_member_staffs as sfs on ap.staffid=sfs.id where sap.staffapplyid=sa.id and sap.status!=0 order by sap.approvaltime desc,sap.id asc),0) as approvalstatus,isnull((select top 1 ap.staffid from oa_approvalprocess_staffapplyapprovals as sap join oa_approvalprocess_approvalpersons as ap on sap.approvalpersonid=ap.id join oa_member_staffs as sfs on ap.staffid=sfs.id where sap.staffapplyid=sa.id and sap.status!=0 order by sap.approvaltime desc,sap.id asc),0) as approvalstaffid from oa_approvalprocess_staffapplys as sa join oa_approvalprocess_projectprocess as pp on sa.projectprocessid=pp.id join oa_approvalprocess_projects as pj on pp.projectid=pj.id join oa_approvalprocess_process as pc on pp.processid=pc.id join oa_member_staffs as sf on sa.staffid=sf.id

--��������Ա�����������б�չʾ��Ϣ��ͼ
create view view_oa_approvalprocess_staffapplyapprovallist
as
select saa.id,saa.companyid,saa.staffapplyid,saa.approvaltime,ap.sort,saa.status,ap.staffid,sf.username from oa_approvalprocess_staffapplyapprovals as saa join oa_approvalprocess_approvalpersons as ap on saa.approvalpersonid=ap.id join oa_member_staffs as sf on ap.staffid=sf.id

--��������Ա�����������ѯ
create view view_oa_approvalprocess_staffapplyall
as
select sa.id,sa.applyname,pp.projectid,pp.processid,sa.companyid,sa.projectprocessid,sa.staffid,ap.staffid as approvalstaffid,saa.approvalpersonid,ap.sort from oa_approvalprocess_staffapplys as sa join oa_approvalprocess_projectprocess as pp on sa.projectprocessid=pp.id join oa_approvalprocess_staffapplyapprovals as saa on sa.id=saa.staffapplyid join oa_approvalprocess_approvalpersons as ap on saa.approvalpersonid=ap.id

--��Ч��ϸ�б�
create view view_oa_achievements_achievementinfolist
as
select ai.id,ai.staffid,ai.institutionid,sf.username,isnull(si.realname,'δ��д') as realname,isnull(it.title,ai.explain) as title,ai.impactscore,ai.impactafterscore,ai.addtime,sf.companyid,sf.departmentid from oa_achievements_achievementsinfo as ai left join oa_achievements_institution as it on ai.institutionid=it.id left join oa_member_staffs as sf on ai.staffid=sf.id left join oa_member_staffsinfos as si on ai.staffid=si.staffid where sf.isachievement=1

--Ա����Ч�б�
create view view_oa_achievements_staffachievementlist
as
select sf.id,sf.username,isnull(si.realname,'δ��д') as realname,dt.departmentname,sf.departmentid,sf.companyid,isnull((select top 1 impactafterscore from oa_achievements_achievementsinfo where staffid=sf.id order by addtime desc),100.00) as score from oa_member_staffs as sf left join oa_member_staffsinfos as si on sf.id=si.staffid left join oa_member_departments as dt on sf.departmentid=dt.id where sf.isachievement=1

--��̳����б�
create view view_oa_forum_section
as
select se.id,se.sectionname,se.sectioninfo,se.companyid,se.staffid,sf.username,se.topiccount,se.addtime,se.status from oa_forum_section as se join oa_member_staffs as sf on se.staffid=sf.id

--��̳�����б�
create view view_oa_forum_topic
as
select tp.id,tp.companyid,tp.sectionid,tp.title,tp.content,tp.staffid,sf.username,tp.addtime,tp.clickcount,tp.lastclicktime,tp.status from oa_forum_topic as tp join oa_member_staffs as sf on tp.staffid=sf.id

--��̳�����б�
create view view_oa_forum_reply
as
select rp.id,rp.companyid,rp.staffid,sf.username,rp.topicid,tp.sectionid,rp.content,rp.addtime,rp.status from oa_forum_reply as rp join oa_forum_topic as tp on rp.topicid=tp.id join oa_member_staffs as sf on rp.staffid=sf.id

--�����б�
create view view_oa_notice_notice
as
select nt.id,nt.typeid,nt.companyid,nt.title,nt.content,nt.staffid,sf.username,nt.sort,nt.edittime,nt.clickcount,nt.commentcount,nt.status from oa_notice_notice as nt join oa_member_staffs as sf on nt.staffid=sf.id

--���������б�
create view view_oa_notice_noticecomment
as
select nc.id,nc.noticeid,nc.companyid,nc.comment,nc.staffid,sf.username,nc.addtime,nc.status from oa_notice_noticecomment as nc join oa_member_staffs as sf on nc.staffid=sf.id

--��̨��˾�б�
create view view_oa_member_companylist
as
select cp.id,cp.companyname,cp.companyinfo,cp.status,cp.addtime,(select count(0) from oa_member_departments where companyid=cp.id) as departmentcount,(select count(0) from oa_member_staffs where companyid=cp.id) as staffcount from oa_member_companys as cp
--��̨Ȩ����־
create view view_oa_power_powerloglist
as
select pl.id,pl.tablename,pl.type,pl.content,pl.auserid,au.username,pl.addip,pl.addtime from oa_power_powerlogs as pl join oa_power_ausers as au on pl.auserid=au.id
--��̨����Ա������־
create view view_oa_power_dologlist
as
select dl.id,dl.auserid,au.username,dl.type,dl.addip,dl.addtime,dl.doinfo from oa_power_dologs as dl left join oa_power_ausers as au on dl.auserid=au.id
--��̨����Ա�б�
create view view_oa_power_auserlist
as
select au.id,au.username,au.password,au.userword,au.roleid,isnull(rl.rolename,'Ĭ��Ȩ��') as rolename,au.status,au.addtime,isnull(ai.realname,'') as realname,isnull(ai.qq,'') as qq,isnull(ai.email,'') as email,isnull(ai.phone,'') as phone from oa_power_ausers as au left join oa_power_ausersinfos as ai on au.id=ai.auserid left join oa_power_roles as rl on au.roleid=rl.id

--��̨��ɫ�����б�
create view view_oa_power_rolefunctionlist
as
select rf.id,rf.roleid,rf.functionid,rf.status,rl.rolename,rl.roleinfo,fu.functionname,fu.functioninfo,fu.parentid from oa_power_rolesfunctions as rf left join oa_power_roles as rl on rf.roleid=rl.id left join oa_power_functions as fu on rf.functionid=fu.id

--��̨����action�б�
create view view_oa_power_functionactionlist
as
select fa.id,fa.actionid,fa.functionid,fa.status,at.actionname,at.actioninfo from oa_power_functionsactions as fa join oa_power_actions as at on fa.actionid=at.id

----------------------�洢���̷ָ���------------------------

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
	if(@@rowcount=1)--�޸�
	begin
		begin tran Tran_auser --��ʼ����
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
			if(@@rowcount>0)--�ж������Ƿ����
			begin
				set @msg='�û����Ѵ��ڣ�';
				set @tran_error = @tran_error+1
			end
			else
			begin
				update oa_power_ausers set username=@username,roleid=@roleid where id=@id
				set @tran_error = @tran_error+@@ERROR
				select * from oa_power_ausersinfos where auserid=@id
				if(@@rowcount=1)--�ж������Ƿ����
				begin
					update oa_power_ausersinfos set realname=@realname,qq=@qq,email=@email,phone=@phone where auserid=@id
					set @tran_error = @tran_error+@@ERROR
				end
				else--������ݲ����ڣ�������
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
			set @msg=@msg+'�޸�ʧ��';
			if(charindex(@msg,'�û����Ѵ���')>=0)
			begin
				--�ύ����
				commit tran Tran_auser;
			end
			else
			begin
				--ִ��ʧ�ܣ��ع�����
				rollback tran Tran_auser;
			end
		end
		else
		begin
			set @result = 1;
			set @msg='�޸ĳɹ�';
			--ִ�гɹ�,�ύ����
			commit tran Tran_auser;
		end
	end
	else--����
	begin
		begin tran Tran_auser --��ʼ����
		set @tran_error=0;
		begin TRY
			select * from oa_power_ausers where username=@username
			if(@@rowcount>1)--�ж������Ƿ����
			begin
				set @msg='�û����Ѵ��ڣ�';
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
			set @msg=@msg+'����ʧ��';
			if(charindex(@msg,'�û����Ѵ���')>=0)
			begin
				--�ύ����
				commit tran Tran_auser;
			end
			else
			begin
				--ִ��ʧ�ܣ��ع�����
				rollback tran Tran_auser;
			end
		end
		else
		begin
			set @result = 1;
			set @msg='�����ɹ�';
			--ִ�гɹ�,�ύ����
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

--����
declare @result int,@msg nvarchar(20)
set @result=-1
set @msg=''

exec proc_updateauser 10,0,'hai','','','������1','971368174','971368174@qq.com','13452579221',@result output,@msg output
print @result
print @msg
----------------------�洢���̷ָ���------------------------

