# main.py
from database import create_connection
from mysql.connector import Error
from prettytable import PrettyTable
import datetime

# --- CÂU 2: HÀM THÊM BỆNH NHÂN VÀ BÁC SĨ ---

def add_patients(conn):
    """Thêm 3 bệnh nhân từ bàn phím"""
    print("\n--- NHẬP THÔNG TIN 3 BỆNH NHÂN ---")
    cursor = conn.cursor()
    sql = "INSERT INTO patients (full_name, date_of_birth, gender, address, phone_number, email) VALUES (%s, %s, %s, %s, %s, %s)"
    
    for i in range(1, 4):
        print(f"Nhập bệnh nhân thứ {i}:")
        full_name = input(" - Họ tên: ")
        dob = input(" - Ngày sinh (YYYY-MM-DD): ")
        gender = input(" - Giới tính: ")
        address = input(" - Địa chỉ: ")
        phone = input(" - SĐT: ")
        email = input(" - Email: ")
        
        val = (full_name, dob, gender, address, phone, email)
        try:
            cursor.execute(sql, val)
            conn.commit()
            print(" -> Đã thêm thành công!")
        except Error as e:
            print(f" -> Lỗi: {e}")

def add_doctors(conn):
    """Thêm 5 bác sĩ từ bàn phím"""
    print("\n--- NHẬP THÔNG TIN 5 BÁC SĨ ---")
    cursor = conn.cursor()
    sql = "INSERT INTO doctors (full_name, specialization, phone_number, email, years_of_experience) VALUES (%s, %s, %s, %s, %s)"
    
    for i in range(1, 6): # Chạy 5 lần
        print(f"Nhập bác sĩ thứ {i}:")
        full_name = input(" - Họ tên: ")
        spec = input(" - Chuyên khoa: ")
        phone = input(" - SĐT: ")
        email = input(" - Email: ")
        exp = input(" - Số năm kinh nghiệm: ")
        
        val = (full_name, spec, phone, email, exp)
        try:
            cursor.execute(sql, val)
            conn.commit()
            print(" -> Đã thêm thành công!")
        except Error as e:
            print(f" -> Lỗi: {e}")

# --- CÂU 3: HÀM THÊM CUỘC HẸN ---

def add_appointments(conn):
    """Thêm 3 cuộc hẹn cho 3 bệnh nhân"""
    print("\n--- TẠO 3 CUỘC HẸN ---")
    cursor = conn.cursor()
    sql = "INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason) VALUES (%s, %s, %s, %s)"
    
    # Để đơn giản và đúng logic bài tập, ta sẽ hiển thị list ID để người dùng chọn
    print("Gợi ý: Hãy nhớ ID của Bệnh nhân và Bác sĩ vừa nhập để tạo lịch hẹn.")
    
    for i in range(1, 4):
        print(f"Nhập cuộc hẹn thứ {i}:")
        p_id = input(" - ID Bệnh nhân: ")
        d_id = input(" - ID Bác sĩ: ")
        date_time = input(" - Thời gian (YYYY-MM-DD HH:MM:SS): ") # Ví dụ: 2023-12-24 09:00:00
        reason = input(" - Lý do khám: ")
        
        val = (p_id, d_id, date_time, reason)
        try:
            cursor.execute(sql, val)
            conn.commit()
            print(" -> Đã đặt lịch thành công!")
        except Error as e:
            print(f" -> Lỗi: {e}")

# --- CÂU 4: TẠO BÁO CÁO (REPORT) ---

def generate_report(conn):
    """Tạo báo cáo theo mẫu trong hình"""
    print("\n--- BÁO CÁO TỔNG HỢP ---")
    cursor = conn.cursor()
    # Query join 3 bảng để lấy đủ thông tin
    sql = """
    SELECT p.patient_id, p.full_name, p.date_of_birth, p.gender, p.address, 
           d.full_name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """
    
    cursor.execute(sql)
    rows = cursor.fetchall()
    
    # Sử dụng PrettyTable để tạo bảng đúng mẫu đề bài
    table = PrettyTable()
    table.field_names = ["No", "Patient name", "Birthday", "Gender", "Address", "Doctor name", "Reason", "Date"]
    
    count = 1
    for row in rows:
        # row[2] là date object, cần ép về string
        table.add_row([
            count, row[1], row[2], row[3], row[4], row[5], row[6], row[7]
        ])
        count += 1
    
    print(table)

# --- CÂU 5: LẤY CUỘC HẸN HÔM NAY ---

def get_appointments_today(conn):
    """Lấy các cuộc hẹn có ngày là hôm nay (CURDATE)"""
    print("\n--- CÁC CUỘC HẸN HÔM NAY ---")
    cursor = conn.cursor()
    
    # Query lọc theo ngày hiện tại
    sql = """
    SELECT p.address, p.patient_id, p.full_name, p.date_of_birth, p.gender, 
           d.full_name, a.status
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = CURDATE()
    """
    
    cursor.execute(sql)
    rows = cursor.fetchall()
    
    table = PrettyTable()
    # Cột 'Note' không có trong DB nên ta để trống như mẫu
    table.field_names = ["Address", "No", "Patient name", "Birthday", "Gender", "Doctor name", "Status", "Note"]
    
    count = 1
    for row in rows:
        table.add_row([
            row[0], count, row[2], row[3], row[4], row[5], row[6], ""
        ])
        count += 1
        
    if not rows:
        print("Không có cuộc hẹn nào trong hôm nay.")
    else:
        print(table)

# --- HÀM MAIN CHÍNH ---

def main():
    conn = create_connection()
    
    if conn is not None and conn.is_connected():
        print("Kết nối Database thành công!")
        
        while True:
            print("\n=== MEDICAL SERVICE SYSTEM ===")
            print("1. Nhập 3 Bệnh nhân (Câu 2)")
            print("2. Nhập 5 Bác sĩ (Câu 2)")
            print("3. Tạo 3 Cuộc hẹn (Câu 3)")
            print("4. Xem Báo cáo tổng hợp (Câu 4)")
            print("5. Xem Cuộc hẹn hôm nay (Câu 5)")
            print("0. Thoát")
            
            choice = input("Chọn chức năng: ")
            
            if choice == '1':
                add_patients(conn)
            elif choice == '2':
                add_doctors(conn)
            elif choice == '3':
                add_appointments(conn)
            elif choice == '4':
                generate_report(conn)
            elif choice == '5':
                get_appointments_today(conn)
            elif choice == '0':
                conn.close()
                print("Đã thoát chương trình.")
                break
            else:
                print("Lựa chọn không hợp lệ.")
    else:
        print("Không thể kết nối Database.")

if __name__ == "__main__":
    main()