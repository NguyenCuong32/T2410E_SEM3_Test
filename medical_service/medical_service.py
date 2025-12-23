import mysql.connector
from mysql.connector import Error
from datetime import datetime, date

DB_CONFIG = {
    "host": "localhost",
    "user": "root",
    "password": "",          
    "database": "medical_service"
}

def get_connection():
    """(1) Connection to database."""
    try:
        conn = mysql.connector.connect(**DB_CONFIG)
        if conn.is_connected():
            return conn
    except Error as e:
        print("Database connection error:", e)
    return None

def input_patient():
    full_name = input("Full name: ").strip()
    dob_str = input("Date of birth (YYYY-MM-DD): ").strip()
    gender = input("Gender (Male/Female/Other): ").strip()
    address = input("Address: ").strip()
    phone = input("Phone number: ").strip()
    email = input("Email: ").strip()

    dob = datetime.strptime(dob_str, "%Y-%m-%d").date()
    return (full_name, dob, gender, address, phone, email)

def input_doctor():
    full_name = input("Doctor full name: ").strip()
    specialization = input("Specialization: ").strip()
    phone = input("Phone number: ").strip()
    email = input("Email: ").strip()
    yoe = int(input("Years of experience: ").strip())
    return (full_name, specialization, phone, email, yoe)

def add_patients_from_keyboard(conn, n=3):
    """(2) Add 3 patients from keyboard."""
    sql = """
        INSERT INTO patients(full_name, date_of_birth, gender, address, phone_number, email)
        VALUES (%s, %s, %s, %s, %s, %s)
    """
    cur = conn.cursor()
    for i in range(n):
        print(f"\n--- Input patient #{i+1} ---")
        data = input_patient()
        cur.execute(sql, data)
    conn.commit()
    print(f"Inserted {n} patient(s).")

def add_doctors_from_keyboard(conn, n=5):
    """(2) Add 5 doctors from keyboard."""
    sql = """
        INSERT INTO doctors(full_name, specialization, phone_number, email, years_of_experience)
        VALUES (%s, %s, %s, %s, %s)
    """
    cur = conn.cursor()
    for i in range(n):
        print(f"\n--- Input doctor #{i+1} ---")
        data = input_doctor()
        cur.execute(sql, data)
    conn.commit()
    print(f"Inserted {n} doctor(s).")

def input_appointment():
    patient_id = int(input("Patient ID: ").strip())
    doctor_id = int(input("Doctor ID: ").strip())
    dt_str = input("Appointment date time (YYYY-MM-DD HH:MM): ").strip()
    reason = input("Reason: ").strip()
    status = input("Status (pending/done/cancelled) [Enter to keep 'pending']: ").strip().lower()

    appt_dt = datetime.strptime(dt_str, "%Y-%m-%d %H:%M")
    if status == "":
        status = "pending"
    return (patient_id, doctor_id, appt_dt, reason, status)

def add_appointments(conn, n=3):
    """(3) Add 3 appointments for 3 patients."""
    sql = """
        INSERT INTO appointments(patient_id, doctor_id, appointment_date, reason, status)
        VALUES (%s, %s, %s, %s, %s)
    """
    cur = conn.cursor()
    for i in range(n):
        print(f"\n--- Input appointment #{i+1} ---")
        data = input_appointment()
        cur.execute(sql, data)
    conn.commit()
    print(f"Inserted {n} appointment(s).")

def report_all_appointments(conn):
    """
    (4) Report with template:
    No | Patient name | Birthday | Gender | Address | Doctor name | Reason | Date
    """
    sql = """
        SELECT p.full_name, p.date_of_birth, p.gender, p.address,
               d.full_name AS doctor_name, a.reason, a.appointment_date
        FROM appointments a
        JOIN patients p ON a.patient_id = p.patient_id
        JOIN doctors d ON a.doctor_id = d.doctor_id
        ORDER BY a.appointment_date ASC
    """
    cur = conn.cursor()
    cur.execute(sql)
    rows = cur.fetchall()

    print("\n========== APPOINTMENTS REPORT ==========")
    header = f"{'No':<4}{'Patient name':<22}{'Birthday':<12}{'Gender':<10}{'Address':<18}{'Doctor name':<22}{'Reason':<20}{'Date':<20}"
    print(header)
    print("-" * len(header))

    for idx, r in enumerate(rows, start=1):
        patient_name, dob, gender, address, doctor_name, reason, appt_dt = r
        dob_str = dob.strftime("%Y-%m-%d") if hasattr(dob, "strftime") else str(dob)
        dt_str = appt_dt.strftime("%Y-%m-%d %H:%M") if hasattr(appt_dt, "strftime") else str(appt_dt)
        print(f"{idx:<4}{patient_name:<22}{dob_str:<12}{gender:<10}{(address or ''):<18}{doctor_name:<22}{(reason or ''):<20}{dt_str:<20}")

def get_today_appointments(conn):
    """
    (5) Get all appointments today and show with template:
    Address | No | Patient name | Birthday | Gender | Doctor name | Status | Note
    Note: đề không có cột note trong DB, mình in Reason làm Note.
    """
    sql = """
        SELECT p.address, p.full_name, p.date_of_birth, p.gender,
               d.full_name AS doctor_name, a.status, a.reason
        FROM appointments a
        JOIN patients p ON a.patient_id = p.patient_id
        JOIN doctors d ON a.doctor_id = d.doctor_id
        WHERE DATE(a.appointment_date) = CURDATE()
        ORDER BY p.address, a.appointment_date
    """
    cur = conn.cursor()
    cur.execute(sql)
    rows = cur.fetchall()

    print("\n========== TODAY APPOINTMENTS ==========")
    header = f"{'Address':<18}{'No':<4}{'Patient name':<22}{'Birthday':<12}{'Gender':<10}{'Doctor name':<22}{'Status':<10}{'Note':<25}"
    print(header)
    print("-" * len(header))

    for idx, r in enumerate(rows, start=1):
        address, patient_name, dob, gender, doctor_name, status, note = r
        dob_str = dob.strftime("%Y-%m-%d") if hasattr(dob, "strftime") else str(dob)
        print(f"{(address or ''):<18}{idx:<4}{patient_name:<22}{dob_str:<12}{gender:<10}{doctor_name:<22}{(status or ''):<10}{(note or ''):<25}")

def menu():
    print("\n==== MEDICAL SERVICE MENU ====")
    print("1. Add 3 patients")
    print("2. Add 5 doctors")
    print("3. Add 3 appointments")
    print("4. Report all appointments")
    print("5. Show today's appointments")
    print("0. Exit")

def main():
    conn = get_connection()
    if not conn:
        print("Cannot connect to DB. Please check MySQL server, DB name, username/password.")
        return

    try:
        while True:
            menu()
            choice = input("Choose: ").strip()
            if choice == "1":
                add_patients_from_keyboard(conn, 3)
            elif choice == "2":
                add_doctors_from_keyboard(conn, 5)
            elif choice == "3":
                add_appointments(conn, 3)
            elif choice == "4":
                report_all_appointments(conn)
            elif choice == "5":
                get_today_appointments(conn)
            elif choice == "0":
                break
            else:
                print("Invalid choice.")
    finally:
        conn.close()
        print("Goodbye!")

if __name__ == "__main__":
    main()
