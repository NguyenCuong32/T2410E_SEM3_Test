import mysql.connector
from datetime import datetime

# ---------------------------------------------------------
# QUESTION 1: Connection to database (1 Mark)
# ---------------------------------------------------------
def get_connection():
    """Thiết lập kết nối tới MySQL database 'medical_service'"""
    try:
        conn = mysql.connector.connect(
            host="localhost",
            user="root",      # Thay đổi theo user của bạn
            password="",      # Thay đổi theo password của bạn
            database="medical_service" # [cite: 17]
        )
        return conn
    except mysql.connector.Error as err:
        print(f"Lỗi kết nối: {err}")
        return None

# ---------------------------------------------------------
# QUESTION 2: Insert 3 Patients & 5 Doctors (6 Marks)
# ---------------------------------------------------------
def insert_data(conn):
    cursor = conn.cursor()
    
    # Thêm 3 bệnh nhân [cite: 9]
    print("--- NHẬP THÔNG TIN 3 BỆNH NHÂN ---")
    for i in range(3):
        print(f"\nBệnh nhân {i+1}:")
        name = input("Họ tên: ")
        dob = input("Ngày sinh (YYYY-MM-DD): ")
        gender = input("Giới tính: ")
        addr = input("Địa chỉ: ")
        phone = input("Số điện thoại: ")
        email = input("Email: ")
        
        sql = "INSERT INTO patients (full_name, date_of_birth, gender, address, phone_number, email) VALUES (%s, %s, %s, %s, %s, %s)"
        cursor.execute(sql, (name, dob, gender, addr, phone, email))
    
    # Thêm 5 bác sĩ [cite: 9]
    print("\n--- NHẬP THÔNG TIN 5 BÁC SĨ ---")
    for i in range(5):
        print(f"\nBác sĩ {i+1}:")
        name = input("Họ tên: ")
        spec = input("Chuyên môn: ")
        phone = input("Số điện thoại: ")
        email = input("Email: ")
        exp = int(input("Số năm kinh nghiệm: "))
        
        sql = "INSERT INTO doctors (full_name, specialization, phone_number, email, years_of_experience) VALUES (%s, %s, %s, %s, %s)"
        cursor.execute(sql, (name, spec, phone, email, exp))
        
    conn.commit()
    print("\n>>> Đã lưu thành công dữ liệu vào Database.")

# ---------------------------------------------------------
# QUESTION 3: Insert 3 Appointments (2 Marks)
# ---------------------------------------------------------
def add_appointments(conn):
    """Thêm 3 lịch hẹn cho 3 bệnh nhân [cite: 10]"""
    cursor = conn.cursor()
    print("\n--- ĐẶT 3 LỊCH HẸN ---")
    for i in range(3):
        print(f"\nLịch hẹn {i+1}:")
        p_id = int(input("ID Bệnh nhân: "))
        d_id = int(input("ID Bác sĩ: "))
        date_str = input("Ngày giờ hẹn (YYYY-MM-DD HH:MM:SS): ")
        reason = input("Lý do: ")
        
        sql = "INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason) VALUES (%s, %s, %s, %s)"
        cursor.execute(sql, (p_id, d_id, date_str, reason))
    
    conn.commit()
    print(">>> Đã thêm lịch hẹn thành công.")

# ---------------------------------------------------------
# QUESTION 4: Make a Report (3 Marks)
# ---------------------------------------------------------
def show_report(conn):
    """Hiển thị báo cáo lịch hẹn theo mẫu [cite: 11, 13]"""
    cursor = conn.cursor()
    query = """
        SELECT a.appointment_id, p.full_name, YEAR(p.date_of_birth), p.gender, 
               p.address, d.full_name, a.reason, a.appointment_date
        FROM appointments a
        JOIN patients p ON a.patient_id = p.patient_id
        JOIN doctors d ON a.doctor_id = d.doctor_id
    """
    cursor.execute(query)
    rows = cursor.fetchall()
    
    print("\n" + "="*100)
    print(f"{'No':<4} | {'Patient Name':<15} | {'Birthday':<8} | {'Gender':<7} | {'Address':<10} | {'Doctor Name':<15} | {'Reason':<10} | {'Date':<10}")
    print("-" * 100)
    for r in rows:
        print(f"{r[0]:<4} | {r[1]:<15} | {r[2]:<8} | {r[3]:<7} | {r[4]:<10} | {r[5]:<15} | {r[6] or '':<10} | {str(r[7]):<10}")

# ---------------------------------------------------------
# QUESTION 5: Get All Appointments Today (2 Marks)
# ---------------------------------------------------------
def get_today_appointments(conn):
    """Hiển thị lịch hẹn của ngày hôm nay [cite: 14, 15]"""
    cursor = conn.cursor()
    today = datetime.now().strftime('%Y-%m-%d')
    query = """
        SELECT p.address, a.appointment_id, p.full_name, YEAR(p.date_of_birth), 
               p.gender, d.full_name, a.status, 'N/A' as note
        FROM appointments a
        JOIN patients p ON a.patient_id = p.patient_id
        JOIN doctors d ON a.doctor_id = d.doctor_id
        WHERE DATE(a.appointment_date) = %s
    """
    cursor.execute(query, (today,))
    rows = cursor.fetchall()
    
    print(f"\n--- DANH SÁCH LỊCH HẸN NGÀY HÔM NAY ({today}) ---")
    print(f"{'Address':<10} | {'No':<4} | {'Patient Name':<15} | {'Birthday':<8} | {'Gender':<7} | {'Doctor Name':<15} | {'Status':<10} | {'Note':<5}")
    print("-" * 100)
    for r in rows:
        print(f"{r[0]:<10} | {r[1]:<4} | {r[2]:<15} | {r[3]:<8} | {r[4]:<7} | {r[5]:<15} | {r[6]:<10} | {r[7]:<5}")

# Main Menu
if __name__ == "__main__":
    connection = get_connection()
    if connection:
        # Gọi các hàm theo yêu cầu đề thi
        insert_data(connection)
        add_appointments(connection)
        show_report(connection)
        get_today_appointments(connection)
        
        connection.close()