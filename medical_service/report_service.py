from datetime import date

def appointment_report(cursor):
    print("\n=== APPOINTMENT REPORT ===")
    sql = """
    SELECT p.full_name, p.date_of_birth, p.gender, p.address,
           d.full_name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """
    cursor.execute(sql)

    for index, row in enumerate(cursor.fetchall(), start=1):
        print(index, row)


def today_appointments(cursor):
    print("\n=== TODAY APPOINTMENTS ===")
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

    for index, row in enumerate(cursor.fetchall(), start=1):
        print(index, row)
