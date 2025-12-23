from db import get_connection

def report_appointments():
    conn = get_connection()
    if conn is None:
        return

    cursor = conn.cursor()

    sql = """
    SELECT
        p.full_name,
        p.date_of_birth,
        p.gender,
        p.address,
        d.full_name AS doctor_name,
        a.reason,
        a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """

    cursor.execute(sql)
    rows = cursor.fetchall()

    print("\n=== REPORT APPOINTMENTS ===")
    print("No | Patient name | Birthday | Gender | Address | Doctor name | Reason | Date")

    no = 1
    for row in rows:
        print(
            f"{no} | {row[0]} | {row[1]} | {row[2]} | {row[3]} | "
            f"{row[4]} | {row[5]} | {row[6]}"
        )
        no += 1

    cursor.close()
    conn.close()

def report_today_appointments():
    conn = get_connection()
    if conn is None:
        return

    cursor = conn.cursor()

    sql = """
    SELECT
        p.address,
        p.full_name,
        p.date_of_birth,
        p.gender,
        d.full_name AS doctor_name,
        a.status,
        a.reason
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE CAST(a.appointment_date AS DATE) = CAST(GETDATE() AS DATE)
    """

    cursor.execute(sql)
    rows = cursor.fetchall()

    print("\n===== APPOINTMENTS TODAY =====")
    print("Address | No | Patient name | Birthday | Gender | Doctor name | Status | Note")

    no = 1
    for row in rows:
        print(
            f"{row[0]} | {no} | {row[1]} | {row[2]} | "
            f"{row[3]} | {row[4]} | {row[5]} | {row[6]}"
        )
        no += 1

    cursor.close()
    conn.close()
