from database import create_connection
import pyodbc
from prettytable import PrettyTable

# --- CÂU 2: THÊM BỆNH NHÂN ---

def add_patients(conn):
    print("\n--- NHẬP THÔNG TIN 3 BỆNH NHÂN ---")
    cursor = conn.cursor()
    sql = """
    INSERT INTO patients
    (full_name, date_of_birth, gender, address, phone_number, email)
    VALUES (?, ?, ?, ?, ?, ?)
    """

    for i in range(1, 4):
        print(f"Nhập bệnh nhân thứ {i}:")
        val = (
            input(" - Họ tên: "),
            input(" - Ngày sinh (YYYY-MM-DD): "),
            input(" - Giới tính: "),
            input(" - Địa chỉ: "),
            input(" - SĐT: "),
            input(" - Email: ")
        )
        try:
            cursor.execute(sql, val)
            conn.commit()
            print(" -> Đã thêm thành công!")
        except pyodbc.Error as e:
            print(f" -> Lỗi: {e}")

# --- THÊM BÁC SĨ ---

def add_doctors(conn):
    print("\n--- NHẬP THÔNG TIN 5 BÁC SĨ ---")
    cursor = conn.cursor()
    sql = """
    INSERT INTO doctors
    (full_name, specialization, phone_number, email, years_of_experience)
    VALUES (?, ?, ?, ?, ?)
    """

    for i in range(1, 6):
        print(f"Nhập bác sĩ thứ {i}:")
        val = (
            input(" - Họ tên: "),
            input(" - Chuyên khoa: "),
            input(" - SĐT: "),
            input(" - Email: "),
            input(" - Số năm kinh nghiệm: ")
        )
        try:
            cursor.execute(sql, val)
            conn.commit()
            print(" -> Đã thêm thành công!")
        except pyodbc.Error as e:
            print(f" -> Lỗi: {e}")

# --- THÊM CUỘC HẸN ---

def add_appointments(conn):
    print("\n--- TẠO 3 CUỘC HẸN ---")
    cursor = conn.cursor()
    sql = """
    INSERT INTO appointments
    (patient_id, doctor_id, appointment_date, reason)
    VALUES (?, ?, ?, ?)
    """

    for i in range(1, 4):
        print(f"Nhập cuộc hẹn thứ {i}:")
        val = (
            input(" - ID Bệnh nhân: "),
            input(" - ID Bác sĩ: "),
            input(" - Thời gian (YYYY-MM-DD HH:MM:SS): "),
            input(" - Lý do khám: ")
        )
        try:
            cursor.execute(sql, val)
            conn.commit()
            print(" -> Đặt lịch thành công!")
        except pyodbc.Error as e:
            print(f" -> Lỗi: {e}")

# --- CÂU 4: REPORT ---

def generate_report(conn):
    cursor = conn.cursor()
    cursor.execute("""
    SELECT p.full_name, p.date_of_birth, p.gender, p.address,
           d.full_name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """)

    table = PrettyTable()
    table.field_names = ["No", "Patient", "Birthday", "Gender", "Address", "Doctor", "Reason", "Date"]

    for i, r in enumerate(cursor.fetchall(), start=1):
        table.add_row([i, *r])

    print(table)

# --- CÂU 5: HÔM NAY ---

def get_appointments_today(conn):
    cursor = conn.cursor()
    cursor.execute("""
    SELECT p.address, p.full_name, p.date_of_birth, p.gender,
           d.full_name, a.status
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE CONVERT(date, a.appointment_date) = CONVERT(date, GETDATE())
    """)

    rows = cursor.fetchall()
    table = PrettyTable()
    table.field_names = ["Address", "Patient", "Birthday", "Gender", "Doctor", "Status", "Note"]

    for r in rows:
        table.add_row([*r, ""])

    print(table if rows else "Không có cuộc hẹn hôm nay.")

# --- MAIN ---

def main():
    conn = create_connection()
    if conn:
        while True:
            print("\n=== MEDICAL SERVICE SYSTEM ===")
            print("1. Nhập 3 Bệnh nhân")
            print("2. Nhập 5 Bác sĩ")
            print("3. Tạo 3 Cuộc hẹn")
            print("4. Báo cáo")
            print("5. Hẹn hôm nay")
            print("0. Thoát")

            c = input("Chọn: ")
            if c == '1': add_patients(conn)
            elif c == '2': add_doctors(conn)
            elif c == '3': add_appointments(conn)
            elif c == '4': generate_report(conn)
            elif c == '5': get_appointments_today(conn)
            elif c == '0':
                conn.close()
                break
    else:
        print("Không kết nối được DB")

if __name__ == "__main__":
    main()
