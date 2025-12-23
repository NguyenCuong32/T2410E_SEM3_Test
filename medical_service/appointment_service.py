from db_connect import get_connection
from datetime import datetime

def add_appointments():
    conn = get_connection()
    cursor = conn.cursor()

    for i in range(3):
        print(f"\nEnter appointment {i + 1}")
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
    print("✔ Appointments added successfully")
def appointment_report():
    conn = get_connection()
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

    print("\nNo | Patient | Birthday | Gender | Address | Doctor | Reason | Date")
    for i, row in enumerate(results, start=1):
        print(i, "|", row[0], "|", row[1], "|", row[2], "|",
              row[3], "|", row[4], "|", row[5], "|", row[6])

    conn.close()
def appointments_today():
    conn = get_connection()
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

    print("\nAddress | Patient | Birthday | Gender | Doctor | Status")
    for row in results:
        print(row)

    conn.close()

