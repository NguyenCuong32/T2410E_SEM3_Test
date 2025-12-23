from db import get_connection

def add_doctors():
    conn = get_connection()
    if conn is None:
        return

    cursor = conn.cursor()

    print("=== NHẬP THÔNG TIN 5 BÁC SĨ ===")

    # Nhập đúng 5 bác sĩ
    for i in range(5):
        print(f"\nBác sĩ thứ {i + 1}")

        full_name = input("Họ tên bác sĩ: ")
        specialization = input("Chuyên khoa: ")
        phone = input("Số điện thoại: ")
        email = input("Email: ")
        experience = input("Số năm kinh nghiệm: ")

        sql = """
        INSERT INTO doctors
        (full_name, specialization, phone_number, email, years_of_experience)
        VALUES (?, ?, ?, ?, ?)
        """

        cursor.execute(
            sql,
            full_name,
            specialization,
            phone,
            email,
            experience
        )

    conn.commit()

    cursor.close()
    conn.close()

    print("\nĐã thêm 5 bác sĩ thành công!")
