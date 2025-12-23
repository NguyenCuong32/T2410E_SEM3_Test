import mysql.connector
from datetime import datetime

def connect_db():
    return mysql.connector.connect(
        host="localhost",
        user="root",
        password="123@abc",   
        database="medical_service"
    )
def add_patients():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(3):
        print(f"\nEnter patient {i+1}")
        name = input("Full name: ")
        dob = input("Date of birth (YYYY-MM-DD): ")
        gender = input("Gender: ")
        address = input("Address: ")

        sql = """
        INSERT INTO patients (full_name, date_of_birth, gender, address)
        VALUES (%s, %s, %s, %s)
        """
        cursor.execute(sql, (name, dob, gender, address))

    conn.commit()
    conn.close()
def add_doctors():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(5):
        print(f"\nEnter doctor {i+1}")
        name = input("Doctor name: ")
        spec = input("Specialization: ")

        sql = """
        INSERT INTO doctors (full_name, specialization)
        VALUES (%s, %s)
        """
        cursor.execute(sql, (name, spec))

    conn.commit()
    conn.close()
def add_appointments():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(3):
        print(f"\nEnter appointment {i+1}")
        patient_id = int(input("Patient ID: "))
        doctor_id = int(input("Doctor ID: "))
        date = input("Appointment date (YYYY-MM-DD HH:MM:SS): ")
        reason = input("Reason: ")

        sql = """
        INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason)
        VALUES (%s, %s, %s, %s)
        """
        cursor.execute(sql, (patient_id, doctor_id, date, reason))

    conn.commit()
    conn.close()
def report_appointments():
    conn = connect_db()
    cursor = conn.cursor()

    sql = """
    SELECT p.full_name, p.date_of_birth, p.gender, p.address,
           d.full_name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """

    cursor.execute(sql)
    results = cursor.fetchall()

    print("\nNo | Patient Name | Birthday | Gender | Address | Doctor | Reason | Date")
    for i, row in enumerate(results, start=1):
        print(i, "|", row[0], "|", row[1], "|", row[2], "|",
              row[3], "|", row[4], "|", row[5], "|", row[6])

    conn.close()
def appointments_today():
    conn = connect_db()
    cursor = conn.cursor()

    today = datetime.now().date()

    sql = """
    SELECT p.address, p.full_name, p.date_of_birth, p.gender,
           d.full_name, a.status
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = %s
    """

    cursor.execute(sql, (today,))
    results = cursor.fetchall()

    print("\nAddress | Patient Name | Birthday | Gender | Doctor | Status")
    for row in results:
        print(row)

    conn.close()

def main():
    while True:
        print("""
1. Add patients
2. Add doctors
3. Add appointments
4. Appointment report
5. Today's appointments
0. Exit
        """)
        choice = input("Choose: ")

        if choice == "1":
            add_patients()
        elif choice == "2":
            add_doctors()
        elif choice == "3":
            add_appointments()
        elif choice == "4":
            report_appointments()
        elif choice == "5":
            appointments_today()
        elif choice == "0":
            break
        else:
            print("Invalid choice!")

main()


