import mysql.connector
from datetime import datetime

# 1. Kết nối tới database
conn = mysql.connector.connect(
    host="localhost",
    port=3306,
    user="root",
    password="root",
    database="medical_service"
)
cursor = conn.cursor()

# 2. Thêm bệnh nhân
patients = [
    ("Nguyen A", "2010-01-01", "Male", "Ha Noi", "0123456789", "a@gmail.com"),
    ("Nguyen B", "1990-05-05", "Female", "Ha Noi", "0987654321", "b@gmail.com"),
    ("Nguyen C", "1985-03-03", "Male", "Hai Phong", "0911222333", "c@gmail.com")
]
sql_patients = """INSERT INTO patients (full_name, date_of_birth, gender, address, phone_number, email)
                  VALUES (%s, %s, %s, %s, %s, %s)"""
cursor.executemany(sql_patients, patients)
conn.commit()
print("Đã thêm bệnh nhân.")

# 3. Thêm bác sĩ
doctors = [
    ("Nguyen Si", "Cardiology", "012345678", "si@gmail.com", 10),
    ("Tran Binh", "Neurology", "098765432", "binh@gmail.com", 8),
    ("Le Hoa", "Pediatrics", "091122233", "hoa@gmail.com", 12),
    ("Pham Long", "Orthopedics", "093344455", "long@gmail.com", 15),
    ("Do Lan", "Dermatology", "094455566", "lan@gmail.com", 7)
]
sql_doctors = """INSERT INTO doctors (full_name, specialization, phone_number, email, years_of_experience)
                 VALUES (%s, %s, %s, %s, %s)"""
cursor.executemany(sql_doctors, doctors)
conn.commit()
print("Đã thêm bác sĩ.")

# 4. Thêm lịch hẹn
appointments = [
    (1, 1, datetime(2025, 12, 23, 10, 0), "Khám tim", "pending"),
    (2, 2, datetime(2025, 12, 23, 14, 0), "Khám thần kinh", "done"),
    (3, 3, datetime(2025, 12, 24, 9, 0), "Khám tổng quát", "pending")
]
sql_appointments = """INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason, status)
                      VALUES (%s, %s, %s, %s, %s)"""
cursor.executemany(sql_appointments, appointments)
conn.commit()
print("Đã thêm lịch hẹn.")

# 5. Báo cáo
cursor.execute("""
    SELECT p.full_name, p.date_of_birth, p.gender, p.address,
           d.full_name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
""")
rows = cursor.fetchall()
print("No | Patient | Birthday | Gender | Address | Doctor | Reason | Date")
for i, row in enumerate(rows, start=1):
    print(i, "|", row[0], "|", row[1], "|", row[2], "|", row[3], "|", row[4], "|", row[5], "|", row[6])

# 6. Lấy lịch hẹn hôm nay
today = datetime.now().date()
cursor.execute("""
    SELECT p.address, p.full_name, p.date_of_birth, p.gender,
           d.full_name, a.status
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = %s
""", (today,))
rows = cursor.fetchall()
print("Address | Patient | Birthday | Gender | Doctor | Status")
for row in rows:
    print(row)

# Đóng kết nối
conn.close()
