def add_patients(cursor):
    for _ in range(3):
        name = input("Patient name: ")
        dob = input("Birthday (YYYY-MM-DD): ")
        gender = input("Gender: ")
        address = input("Address: ")
        cursor.execute(
            """
            INSERT INTO patients (full_name, date_of_birth, gender, address)
            VALUES (%s, %s, %s, %s)
            """,
            (name, dob, gender, address)
        )
