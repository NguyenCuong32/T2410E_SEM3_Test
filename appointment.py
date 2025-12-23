from db import connect_db

def add_appointments():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(3):
        print(f"\nNhập lịch hẹn {i+1}")
        patient_id = int(input("Patient ID: "))
        doctor_id = int(input("Doctor ID: "))
        date = input("Ngày giờ (YYYY-MM-DD HH:MM:SS): ")
        reason = input("Lý do: ")

        sql = """
        INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason)
        VALUES (%s, %s, %s, %s)
        """
        cursor.execute(sql, (patient_id, doctor_id, date, reason))

    conn.commit()
    conn.close()
    print("✅ Đã thêm 3 lịch hẹn")
