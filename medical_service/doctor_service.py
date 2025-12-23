def insert_doctors(cursor):
    print("\n=== Enter 5 Doctors ===")
    for i in range(5):
        print(f"\nDoctor {i + 1}")
        full_name = input("Full name: ")
        specialization = input("Specialization: ")
        phone = input("Phone: ")
        email = input("Email: ")
        experience = int(input("Years of experience: "))

        sql = """
        INSERT INTO doctors
        (full_name, specialization, phone_number, email, years_of_experience)
        VALUES (%s, %s, %s, %s, %s)
        """
        cursor.execute(sql, (full_name, specialization, phone, email, experience))
