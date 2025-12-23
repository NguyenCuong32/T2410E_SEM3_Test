import mysql.connector
from datetime import datetime, date

# 1. Connection to database
def connect_db():
    return mysql.connector.connect(
        host="localhost",
        user="root",
        password="",   # đổi nếu MySQL của bạn khác
        database="medical_service"
    )

# 2. Add patients
def add_patients(cursor):
    print("=== Add 3 Patients ===")
    for i in range(3):
        name = input("Full name: ")
        dob = input("Date of birth (YYYY-MM-DD): ")
        gender = input("Gender: ")
        address = input("Address: ")
        phone = input("Phone: ")
        email = input("Email: ")

        sql = """
        INSERT INTO patients(full_name, date_of_birth, gender, address, phone_number, email)
        VALUES (%s, %s, %s, %s, %s, %s)
        """
        cursor.execute(sql, (name, dob, gender, address, phone, email))

# 2. Add doctors
def add_doctors(cursor):
    print("=== Add 5 Doctors ===")
    for i in range(5):
        name = input("Doctor name: ")
        spec = input("Specialization: ")
        phone = input("Phone: ")
        email = input("Email: ")
        exp = int(input("Years of experience: "))

        sql = """
        INSERT INTO doctors(full_name, specialization, phone_number, email, years_of_experience)
        VALUES (%s, %s, %s, %s, %s)
        """
        cursor.execute(sql, (name, spec, phone, email, exp))

# 3. Add appointments
def add_appointments(cursor):
    print("=== Add 3 Appointments ===")
    for i in range(3):
        patient_id = int(input("Patient ID: "))
        doctor_id = int(input("Doctor ID: "))
        app_date = input("Appointment datetime (YYYY-MM-DD HH:MM:SS): ")
        reason = input("Reason: ")

        sql = """
        INSERT INTO appointments(patient_id, doctor_id, appointment_date, reason)
        VALUES (%s, %s, %s, %s)
        """
        cursor.execute(sql, (patient_id, doctor_id, app_date, reason))

# 4. Report
def report_appointments(cursor):
    print("\n=== Appointment Report ===")
    sql = """
    SELECT p.patient_id, p.full_name, p.date_of_birth, p.gender, p.address,
           d.full_name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """
    cursor.execute(sql)
    rows = cursor.fetchall()

    print("No | Patient Name | Birthday | Gender | Address | Doctor | Reason | Date")
    for i, r in enumerate(rows, start=1):
        print(f"{i} | {r[1]} | {r[2]} | {r[3]} | {r[4]} | {r[5]} | {r[6]} | {r[7]}")

# 5. Get all appointments today
def appointments_today(cursor):
    print("\n=== Appointments Today ===")
    today = date.today()

    sql = """
    SELECT p.address, p.patient_id, p.full_name, p.date_of_birth,
           p.gender, d.full_name, a.status, a.reason
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = %s
    """
    cursor.execute(sql, (today,))
    rows = cursor.fetchall()

    print("Address | ID | Name | Birthday | Gender | Doctor | Status | Note")
    for r in rows:
        print(f"{r[0]} | {r[1]} | {r[2]} | {r[3]} | {r[4]} | {r[5]} | {r[6]} | {r[7]}")

# Main
def main():
    conn = connect_db()
    cursor = conn.cursor()

    add_patients(cursor)
    add_doctors(cursor)
    add_appointments(cursor)

    conn.commit()

    report_appointments(cursor)
    appointments_today(cursor)

    cursor.close()
    conn.close()

if __name__ == "__main__":
    main()
