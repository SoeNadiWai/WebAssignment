CREATE DATABASE Charity

select * from AspNetUsers
INSERT dbo.AspNetRoles values ('3','User')
INSERT dbo.AspNetUserRoles values ('8f3fa659-e752-4c3d-8842-4fb64ff1e4a6','3')

---Add Trigger-------------
CREATE TRIGGER trg_AspNetUserRoles
ON dbo.AspNetUsers
After INSERT
AS
declare @orgtype varchar
set @orgtype=(select OrgType from inserted)
declare @id nvarchar(36)
set @id=(select Id from inserted)
if (@orgtype is not null)
INSERT AspNetUserRoles VALUES (@id,'2');
else
INSERT AspNetUserRoles Values (@id,'3')


---CREATE TABLES-----

CREATE TABLE Event(
EventID int not null,
Subject nvarchar(100),
Description nvarchar(300),
Startdate datetime,
Enddate datetime,
Themecolor nvarchar(10),
Isfullday bit,
RequestedOrgName NVARCHAR (256) not null,
CONSTRAINT PK_Event
PRIMARY KEY (EventID)
)

CREATE TABLE CharitablePlaces(
PID int not null,
Pname varchar(30),
RequestedOrgName nvarchar(256),
RequestStartDate datetime,
RequestEndDate datetime,
Addresss nvarchar (50),
Placelat varchar(20),
Placelong varchar(20),
Description nvarchar(500),
image varchar(300),
CONSTRAINT PK_CharitablePlaces
PRIMARY KEY (PID)
)


select * from Donation
CREATE TABLE Donation(
DonationID int not null,
UserID nvarchar(128) not null,
UserName nvarchar(20) not null,
OrgName nvarchar(50),
DonateAmount int,
DonateDate date,
CONSTRAINT PK_Donation
PRIMARY KEY(DonationID)
)

CREATE TABLE Volunteer(
VID int not null,
UserID nvarchar(128) not null,
VName nvarchar(50) not null,
VEmail nvarchar(50) not null,
VGender nvarchar(10) not null,
VPreferType nvarchar(50),
CONSTRAINT PK_Volunteer
PRIMARY KEY(VID)
)

-----ADD FOREIGN KEYS------

ALTER TABLE Donation
ADD CONSTRAINT FK_Donation_AspNetUsers
FOREIGN KEY(UserID)
REFERENCES AspNetUsers(Id)
ON DELETE CASCADE
ON UPDATE CASCADE

ALTER TABLE Volunteer
ADD CONSTRAINT FK_Volunteer_AspNetUsers
FOREIGN KEY(UserID)
REFERENCES AspNetUsers(Id)
ON DELETE CASCADE
ON UPDATE CASCADE

ALTER TABLE CharitablePlaces
ADD CONSTRAINT FK_CharitablePlaces_AspNetUsers
FOREIGN KEY(RequestedOrgName)
REFERENCES AspNetUsers(UserName)
ON DELETE CASCADE
ON UPDATE CASCADE



ALTER TABLE Event
ADD CONSTRAINT FK_Event_AspNetUsers
FOREIGN KEY(RequestedOrgName)
REFERENCES AspNetUsers(UserName)
ON DELETE CASCADE
ON UPDATE CASCADE


----insert procedure-----
CREATE PROCEDURE [dbo].[Validate_LoginUser]
      @Username NVARCHAR(256)
AS
BEGIN
      SET NOCOUNT ON;
      DECLARE @UserId NVARCHAR(128)    
      SELECT @UserId = Id FROM AspNetUsers WHERE UserName = @Username 
      
      IF @UserId IS NOT NULL
      BEGIN
           -- IF NOT EXISTS(SELECT UserId FROM UserActivation WHERE UserId = @UserId)
            --BEGIN
              --    UPDATE UserTable
                --  SET LastLoginDate = GETDATE()
                  --WHERE UserId = @UserId
                  --SELECT @UserId [UserId] -- User Valid
            --END
            --ELSE
            --BEGIN
                 -- SELECT -2 -- User not activated.
           -- END
		   SELECT -1  
      END

      BEGIN
            SELECT -1 -- User invalid.
      END
	  END

	  delete AspNetUsers where Id='27e9deee-b29b-4e90-9b8e-b8e85eadf4b7'

	select * from Volunteer
	drop procedure [dbo].[Validate_LoginUser]
	select * from AspNetUsers

---inserting data-----------
INSERT EVENT values (1,'Student Talk','Students will offer peer discussion!','2-25-2019 3:40 PM','2-26-2019 3:40 PM','green',0,'VSO Myanmar')
INSERT EVENT values (2,'Flood Victim Fun Raising','Funds for victims in Ayawaddy District will be gathered!','3-5-2019 3:40 PM','3-10-2019 3:40 PM','blue',0,'VSO Myanmar')
INSERT EVENT values (3,'Net Con Fair','Can volunteer and get experiences at this expo show','3-14-2019 3:40 PM','3-17-2019 3:40 PM','red',0,'VSO Myanmar')
INSERT EVENT values (4,'Bar Camp','Can volunteer and get experiences at this barcamp','2-14-2019 3:40 PM','2-22-2019 3:40 PM','black',0,'VSO Myanmar')


INSERT CharitablePlaces values (1,'Myit Sone','VSO Myanmar','2-23-2019 3:40 PM','2-24-2019 2:00 PM','Kachin State','25.388104','97.370727','People around Myit Sone Area needs donations due to the force removal of their houses!','http://www.asianews.it/files/img/MYANMAR_-_karuna_profughi_ok.jpg')
INSERT CharitablePlaces values (2,'Rakhine','Earthpreneurs Myanmar','2-26-2019 3:40 PM','2-27-2019 2:00 PM','Rakhine State','25.388104','97.370727','People around Myit Sone Area needs donations due to the force removal of their houses!','http://salem-news.com/stimg/october062012/rohingyas_victims.jpg')
