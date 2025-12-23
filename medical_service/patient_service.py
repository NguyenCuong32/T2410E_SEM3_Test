def insert_patients(cursor):
    for i in range(3):
        print(f"\nPatient {i+1}")
        full_name = input("Full name: ")
        dob = input("Date of birth (YYYY-MM-DD): ")
        gender = input("Gender: ")
        address = input("Address: ")
        phone = input("Phone: ")
        email = input("Email: ")

        sql = """
        INSERT INTO Patients
        (full_name, date_of_birth, gender, address, phone_number, email)
        VALUES (?, ?, ?, ?, ?, ?)
        """

        cursor.execute(sql, (full_name, dob, gender, address, phone, email))
