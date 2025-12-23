from db import connect_db

def report_all():
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
    rows = cursor.fetchall()

    print("\nNo | Patient | DOB | Gender | Address | Doctor | Reason | Date")
    for i, r in enumerate(rows, start=1):
        print(i, r)

    conn.close()


def appointments_today():
    conn = connect_db()
    cursor = conn.cursor()

    sql = """
    SELECT p.address, p.full_name, p.date_of_birth, p.gender,
           d.full_name, a.status
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = CURDATE()
    """

    cursor.execute(sql)
    rows = cursor.fetchall()

    print("\nAddress | Patient | DOB | Gender | Doctor | Status")
    for r in rows:
        print(r)

    conn.close()
