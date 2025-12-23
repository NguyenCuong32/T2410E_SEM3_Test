def add_patient(cursor):
    print("\n--- NHẬP BỆNH NHÂN ---")
    val = (
        input("Họ tên: "),
        input("Ngày sinh (YYYY-MM-DD): "),
        input("Giới tính: "),
        input("Địa chỉ: "),
        input("SĐT: "),
        input("Email: ")
    )
    cursor.execute("INSERT INTO patients (full_name, date_of_birth, gender, address, phone_number, email) VALUES (?, ?, ?, ?, ?, ?)", val)

def add_doctor(cursor):
    print("\n--- NHẬP BÁC SĨ ---")
    val = (
        input("Họ tên BS: "),
        input("Chuyên khoa: "),
        input("SĐT: "),
        input("Email: "),
        input("Số năm KN: ")
    )
    cursor.execute("INSERT INTO doctors (full_name, specialization, phone_number, email, years_of_experience) VALUES (?, ?, ?, ?, ?)", val)

def input_data_batch(conn):
    cursor = conn.cursor()
    print("=== YÊU CẦU: NHẬP 3 BỆNH NHÂN ===")
    for i in range(3):
        print(f"-> Bệnh nhân {i+1}:")
        add_patient(cursor)
    
    print("\n=== YÊU CẦU: NHẬP 5 BÁC SĨ ===")
    for i in range(5):
        print(f"-> Bác sĩ {i+1}:")
        add_doctor(cursor)
    conn.commit()
    print("Đã lưu dữ liệu thành công!")

def input_appointments_batch(conn):
    cursor = conn.cursor()
    print("\n=== YÊU CẦU: NHẬP 3 LỊCH HẸN ===")
    for i in range(3):
        print(f"-> Lịch hẹn {i+1}:")
        try:
            val = (
                int(input("ID Bệnh nhân: ")),
                int(input("ID Bác sĩ: ")),
                input("Ngày giờ (YYYY-MM-DD HH:MM:SS): "),
                input("Lý do khám: ")
            )
            cursor.execute("INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason, status) VALUES (?, ?, ?, ?, 'pending')", val)
        except Exception as e:
            print(f"Lỗi nhập liệu: {e}")
    conn.commit()
    print("Đã lưu lịch hẹn!")