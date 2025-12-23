import mysql.connector
from mysql.connector import Error
from prettytable import PrettyTable

# ================== CÂU 1: KẾT NỐI DATABASE ==================

def create_connection():
    try:
        conn = mysql.connector.connect(
            host="localhost",
            user="root",
            password="root",          # MAMP mặc định
            database="medical_service",
            port=3306                 # ⚠️ đổi thành 8889 nếu MAMP dùng port này
        )
        return conn
    except Error as e:
        print("Lỗi kết nối database:", e)
        return None

# ================== CÂU 2: THÊM PATIENTS ==================

def add_patients(conn):
    print("\n--- NHẬP 3 BỆNH NHÂN ---")
    cursor = conn.cursor()
    sql = """
    INSERT INTO patients (full_name, date_of_birth, gender, address, phone_number, email)
    VALUES (%s, %s, %s, %s, %s, %s)
    """

    for i in range(1, 4):
        print(f"Bệnh nhân {i}:")
        name = input("Họ tên: ")
        dob = input("Ngày sinh (YYYY-MM-DD): ")
        gender = input("Giới tính: ")
        address = input("Địa chỉ: ")
        phone = input("SĐT: ")
        email = input("Email: ")

        cursor.execute(sql, (name, dob, gender, address, phone, email))

    conn.commit()
    print("✔ Đã thêm 3 bệnh nhân")

# ================== CÂU 2: THÊM DOCTORS ==================

def add_doctors(conn):
    print("\n--- NHẬP 5 BÁC SĨ ---")
    cursor = conn.cursor()
    sql = """
    INSERT INTO doctors (full_name, specialization, phone_number, email, years_of_experience)
    VALUES (%s, %s, %s, %s, %s)
    """

    for i in range(1, 6):
        print(f"Bác sĩ {i}:")
        name = input("Họ tên: ")
        spec = input("Chuyên khoa: ")
        phone = input("SĐT: ")
        email = input("Email: ")
        exp = input("Số năm kinh nghiệm: ")

        cursor.execute(sql, (name, spec, phone, email, exp))

    conn.commit()
    print("✔ Đã thêm 5 bác sĩ")

# ================== CÂU 3: THÊM APPOINTMENTS ==================

def add_appointments(conn):
    print("\n--- TẠO 3 CUỘC HẸN ---")
    cursor = conn.cursor()
    sql = """
    INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason)
    VALUES (%s, %s, %s, %s)
    """

    for i in range(1, 4):
        print(f"Cuộc hẹn {i}:")
        patient_id = input("Patient ID: ")
        doctor_id = input("Doctor ID: ")
        date_time = input("Thời gian (YYYY-MM-DD HH:MM:SS): ")
        reason = input("Lý do khám: ")

        cursor.execute(sql, (patient_id, doctor_id, date_time, reason))

    conn.commit()
    print("✔ Đã tạo 3 cuộc hẹn")

# ================== CÂU 4: REPORT ==================

def generate_report(conn):
    print("\n--- BÁO CÁO TỔNG HỢP ---")
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

    table = PrettyTable()
    table.field_names = ["No", "Patient name", "Birthday", "Gender",
                         "Address", "Doctor name", "Reason", "Date"]

    for i, row in enumerate(rows, start=1):
        table.add_row([i, row[0], row[1], row[2], row[3], row[4], row[5], row[6]])

    print(table)

# ================== CÂU 5: APPOINTMENTS TODAY ==================

def get_appointments_today(conn):
    print("\n--- CUỘC HẸN HÔM NAY ---")
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

    table = PrettyTable()
    table.field_names = ["Address", "No", "Patient name", "Birthday",
                         "Gender", "Doctor name", "Status", "Note"]

    for i, row in enumerate(rows, start=1):
        table.add_row([row[0], i, row[1], row[2], row[3], row[4], row[5], ""])

    if rows:
        print(table)
    else:
        print("Không có cuộc hẹn nào hôm nay.")

# ================== MAIN MENU ==================

def main():
    conn = create_connection()
    if conn is None or not conn.is_connected():
        print("❌ Không kết nối được database")
        return

    print("✅ Kết nối database thành công")

    while True:
        print("\n=== MEDICAL SERVICE SYSTEM ===")
        print("1. Nhập 3 bệnh nhân")
        print("2. Nhập 5 bác sĩ")
        print("3. Tạo 3 cuộc hẹn")
        print("4. Xem báo cáo")
        print("5. Xem cuộc hẹn hôm nay")
        print("0. Thoát")

        choice = input("Chọn chức năng: ")

        if choice == "1":
            add_patients(conn)
        elif choice == "2":
            add_doctors(conn)
        elif choice == "3":
            add_appointments(conn)
        elif choice == "4":
            generate_report(conn)
        elif choice == "5":
            get_appointments_today(conn)
        elif choice == "0":
            conn.close()
            print("Đã thoát chương trình")
            break
        else:
            print("Lựa chọn không hợp lệ")

# ================== RUN ==================

if __name__ == "__main__":
    main()
