import mysql.connector
from datetime import datetime

# ====== CONNECT DATABASE (PHẢI Ở TRÊN) ======
def connect_db():
    return mysql.connector.connect(
        host="localhost",
        user="root",
        password="root",
        database="medical_service",
        port=3306   # MAMP Windows
    )

print("Connected MySQL OK")

def insert_doctors():
    db = connect_db()
    cursor = db.cursor()

    for i in range(5):
        print(f"\nDoctor {i+1}")
        name = input("Full name: ")
        spec = input("Specialization: ")
        phone = input("Phone: ")
        email = input("Email: ")
        exp = int(input("Years of experience: "))

        sql = """
        INSERT INTO doctors(full_name, specialization, phone_number, email, years_of_experience)
        VALUES (%s,%s,%s,%s,%s)
        """
        cursor.execute(sql, (name, spec, phone, email, exp))

    db.commit()
    db.close()


def insert_patients():
    db = connect_db()
    cursor = db.cursor()

    for i in range(3):
        print(f"\nPatient {i+1}")
        name = input("Full name: ")
        dob = input("Date of birth (YYYY-MM-DD): ")
        gender = input("Gender: ")
        address = input("Address: ")
        phone = input("Phone: ")
        email = input("Email: ")

        sql = """
        INSERT INTO patients(full_name, date_of_birth, gender, address, phone_number, email)
        VALUES (%s,%s,%s,%s,%s,%s)
        """
        cursor.execute(sql, (name, dob, gender, address, phone, email))

    db.commit()
    db.close()

def create_appointments():
    db = connect_db()
    cursor = db.cursor()

    for i in range(3):
        print(f"\nAppointment {i+1}")
        patient_id = int(input("Patient ID: "))
        doctor_id = int(input("Doctor ID: "))
        date = input("Appointment date (YYYY-MM-DD HH:MM): ")
        reason = input("Reason: ")

        sql = """
        INSERT INTO appointments(patient_id, doctor_id, appointment_date, reason)
        VALUES (%s,%s,%s,%s)
        """
        cursor.execute(sql, (patient_id, doctor_id, date, reason))

    db.commit()
    db.close()

def report_all_appointments():
    db = connect_db()
    cursor = db.cursor()

    sql = """
    SELECT p.full_name, p.date_of_birth, p.gender, p.address,
           d.full_name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """

    cursor.execute(sql)
    results = cursor.fetchall()

    print("\nNo | Patient | Birth | Gender | Address | Doctor | Reason | Date")
    print("-"*80)

    for i, row in enumerate(results, start=1):
        print(f"{i} | {row[0]} | {row[1]} | {row[2]} | {row[3]} | {row[4]} | {row[5]} | {row[6]}")

    db.close()

def get_today_appointments():
    db = connect_db()
    cursor = db.cursor()

    sql = """
    SELECT
        p.address,
        p.full_name,
        p.date_of_birth,
        p.gender,
        d.full_name,
        a.status
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = CURDATE()
    """

    cursor.execute(sql)
    results = cursor.fetchall()

    print("\nGet all appointments today")
    print("-" * 95)
    print("{:<10} {:<5} {:<15} {:<10} {:<8} {:<15} {:<10} {:<10}".format(
        "Address", "No", "Patient name", "Birthday", "Gender",
        "Doctor name", "Status", "Note"
    ))
    print("-" * 95)

    for i, row in enumerate(results, start=1):
        address, name, dob, gender, doctor, status = row
        print("{:<10} {:<5} {:<15} {:<10} {:<8} {:<15} {:<10} {:<10}".format(
            address,
            i,
            name,
            dob.year,          # chỉ lấy năm sinh (2010, 1990)
            gender,
            doctor,
            status.capitalize(),
            ""                 # Note để trống
        ))

    db.close()


if __name__ == "__main__":
    insert_doctors()
    insert_patients()
    create_appointments()
    report_all_appointments()
    get_today_appointments()
