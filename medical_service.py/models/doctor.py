from config.db import connect_db

def add_doctors():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(5):
        print(f"\nNhập bác sĩ {i+1}")
        name = input("Tên: ")
        specialization = input("Chuyên khoa: ")
        phone = input("SĐT: ")
        email = input("Email: ")
        experience = int(input("Số năm kinh nghiệm: "))

        sql = """
        INSERT INTO doctors(full_name, specialization, phone_number, email, years_of_experience)
        VALUES (%s, %s, %s, %s, %s)
        """
        cursor.execute(sql, (name, specialization, phone, email, experience))

    conn.commit()
    conn.close()
    print("✔ Đã thêm 5 bác sĩ")
