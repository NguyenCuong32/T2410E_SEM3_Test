from db import get_connection

def add_appointments():
    conn = get_connection()
    if conn is None:
        return

    cursor = conn.cursor()

    print("=== NHẬP 3 LỊCH HẸN KHÁM ===")

    for i in range(3):
        print(f"\nLịch hẹn thứ {i + 1}")

        patient_id = input("Nhập ID Bệnh Nhân: ")
        doctor_id = input("Nhập ID Bác Sĩ: ")
        appointment_date = input("Ngày khám (YYYY-MM-DD HH:MM): ")
        reason = input("Lý do khám: ")

        sql = """
        INSERT INTO appointments
        (patient_id, doctor_id, appointment_date, reason)
        VALUES (?, ?, ?, ?)
        """

        cursor.execute(
            sql,
            patient_id,
            doctor_id,
            appointment_date,
            reason
        )

    conn.commit()
    cursor.close()
    conn.close()

    print("\nĐã thêm 3 lịch hẹn thành công!")
