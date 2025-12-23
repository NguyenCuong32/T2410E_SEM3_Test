from db import connect_db

def add_doctors():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(5):
        print(f"\nNhập bác sĩ {i+1}")
        name = input("Tên bác sĩ: ")
        spec = input("Chuyên khoa: ")
        phone = input("SĐT: ")
        email = input("Email: ")
        exp = int(input("Số năm kinh nghiệm: "))

        sql = """
        INSERT INTO doctors (full_name, specialization, phone_number, email, years_of_experience)
        VALUES (%s, %s, %s, %s, %s)
        """
        cursor.execute(sql, (name, spec, phone, email, exp))

    conn.commit()
    conn.close()
    print("✅ Đã thêm 5 bác sĩ")
