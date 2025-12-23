from config.db import connect_db

def add_patients():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(3):
        print(f"\nNhập bệnh nhân {i+1}")
        name = input("Tên: ")
        dob = input("Ngày sinh (YYYY-MM-DD): ")
        gender = input("Giới tính: ")
        address = input("Địa chỉ: ")
        phone = input("SĐT: ")
        email = input("Email: ")

        sql = """
        INSERT INTO patients(full_name, date_of_birth, gender, address, phone_number, email)
        VALUES (%s, %s, %s, %s, %s, %s)
        """
        cursor.execute(sql, (name, dob, gender, address, phone, email))

    conn.commit()
    conn.close()
    print("✔ Đã thêm 3 bệnh nhân")
