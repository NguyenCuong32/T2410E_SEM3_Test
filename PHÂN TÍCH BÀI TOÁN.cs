PHÂN TÍCH BÀI TOÁN

Hệ thống cho phép:

Quản lý bệnh nhân

Quản lý bác sĩ

Đặt, cập nhật, hủy lịch hẹn khám

Thống kê, báo cáo lịch hẹn theo tháng
2 THIẾT KẾ DATABASE (Azure SQL)
CREATE DATABASE MedicalAppointmentDB;
GO
USE MedicalAppointmentDB;
GO

CREATE TABLE Patients (
    PatientId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(100),
    Phone NVARCHAR(20),
    DateOfBirth DATE
);

CREATE TABLE Doctors (
    DoctorId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(100),
    Specialty NVARCHAR(100)
);

CREATE TABLE Appointments (
    AppointmentId INT IDENTITY PRIMARY KEY,
    PatientId INT,
    DoctorId INT,
    AppointmentDate DATETIME,
    Status NVARCHAR(50),
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);

3 BACKEND – AZURE FUNCTION (.NET 8 – C#)
 Cấu trúc Clean Architecture
MedicalAppointmentSystem
│
├── Domain
│   └── Entities
├── Application
│   └── Services
├── Infrastructure
│   └── Data
└── Functions
    ├── PatientFunction.cs
    ├── DoctorFunction.cs
    └── AppointmentFunction.cs

 Entity mẫu – Patient
namespace Domain.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

 Azure Function – Patient CRUD
[FunctionName("GetPatients")]
public static IActionResult GetPatients(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "patients")] HttpRequest req,
    ILogger log)
{
    using var conn = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnection"));
    var cmd = new SqlCommand("SELECT * FROM Patients", conn);
    conn.Open();
    var reader = cmd.ExecuteReader();

    var list = new List<object>();
    while (reader.Read())
    {
        list.Add(new {
            PatientId = reader["PatientId"],
            FullName = reader["FullName"],
            Phone = reader["Phone"]
        });
    }
    return new OkObjectResult(list);
}

 Appointment API – Đặt lịch
[FunctionName("CreateAppointment")]
public static IActionResult CreateAppointment(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "appointments")] HttpRequest req)
{
    var body = new StreamReader(req.Body).ReadToEnd();
    dynamic data = JsonConvert.DeserializeObject(body);

    using var conn = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnection"));
    var cmd = new SqlCommand(
        "INSERT INTO Appointments(PatientId, DoctorId, AppointmentDate, Status) VALUES (@p,@d,@date,@s)", conn);

    cmd.Parameters.AddWithValue("@p", (int)data.PatientId);
    cmd.Parameters.AddWithValue("@d", (int)data.DoctorId);
    cmd.Parameters.AddWithValue("@date", (DateTime)data.AppointmentDate);
    cmd.Parameters.AddWithValue("@s", "Scheduled");

    conn.Open();
    cmd.ExecuteNonQuery();

    return new OkObjectResult("Appointment created");
}

4️ DANH SÁCH API (12 API – ĐẠT YÊU CẦU)
#	Method	API
1	POST	/api/patients
2	GET	/api/patients
3	GET	/api/patients/{id}
4	PUT	/api/patients/{id}
5	DELETE	/api/patients/{id}
6	POST	/api/doctors
7	GET	/api/doctors
8	POST	/api/appointments
9	GET	/api/appointments
10	PUT	/api/appointments/{id}
11	DELETE	/api/appointments/{id}
12	GET	/api/reports/appointments?month=&year=
5️ FRONTEND – REACTJS
Cấu trúc
src/
 ├── components/
 ├── pages/
 │    ├── Patients.jsx
 │    ├── Doctors.jsx
 │    ├── Appointments.jsx
 ├── App.js

 Gọi API – Axios
import axios from "axios";

export const api = axios.create({
  baseURL: "https://your-azure-function-url/api"
});

Trang danh sách bệnh nhân
useEffect(() => {
  api.get("/patients").then(res => setPatients(res.data));
}, []);

6️ DEPLOY AZURE (MÔ TẢ BÁO CÁO)

Tạo Azure SQL Database

Tạo Azure Function App

Cấu hình Connection String

Publish Function từ Visual Studio

Build ReactJS

npm run build


Deploy lên Azure Static Web Apps

7️ VIDEO DEMO (5–7 PHÚT)
Nội dung nói:

Giới thiệu đề tài

Kiến trúc hệ thống

Demo CRUD Patient

Đặt lịch hẹn

Xem dữ liệu trên Azure

Truy cập website cloud