from config.db import connect_db
from datetime import date

def report():
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

    print("\n===== REPORT =====")
    print("No | Patient | Birthday | Gender | Address | Doctor | Reason | Date")

    for i, row in enumerate(cursor.fetchall(), start=1):
        print(i, row)

    conn.close()


def appointments_today():
    conn = connect_db()
    cursor = conn.cursor()

    today = date.today()

    sql = """
    SELECT p.address, p.full_name, p.date_of_birth, p.gender,
           d.full_name, a.status
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = %s
    """
    cursor.execute(sql, (today,))

    print("\n===== APPOINTMENTS TODAY =====")
    print("Address | Patient | Birthday | Gender | Doctor | Status")

    for row in cursor.fetchall():
        print(row)

    conn.close()
