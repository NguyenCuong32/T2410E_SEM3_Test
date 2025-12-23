from db.database import connect_db, connect_server

def add_patients():
    db = connect_db()
    cursor = db.cursor()

    for i in range(3):
        print(f"\nEnter patient {i+1}")
        name = input("Full name: ")
        dob = input("Date of birth (YYYY-MM-DD): ")
        gender = input("Gender: ")
        address = input("Address: ")

        sql = """
        INSERT INTO patients (full_name, date_of_birth, gender, address)
        VALUES (%s, %s, %s, %s)
        """
        cursor.execute(sql, (name, dob, gender, address))

    db.commit()
    db.close()
    print("✔ 3 patients added")

def add_doctors():
    db = connect_db()
    cursor = db.cursor()

    for i in range(5):
        print(f"\nEnter doctor {i+1}")
        name = input("Doctor name: ")
        spec = input("Specialization: ")

        sql = """
        INSERT INTO doctors (full_name, specialization)
        VALUES (%s, %s)
        """
        cursor.execute(sql, (name, spec))

    db.commit()
    db.close()
    print("✔ 5 doctors added")

def add_appointments():
    db = connect_db()
    cursor = db.cursor()

    for i in range(3):
        print(f"\nEnter appointment {i+1}")
        patient_id = int(input("Patient ID: "))
        doctor_id = int(input("Doctor ID: "))
        reason = input("Reason: ")
        date_time = input("Appointment date (YYYY-MM-DD HH:MM): ")

        sql = """
        INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason)
        VALUES (%s, %s, %s, %s)
        """
        cursor.execute(sql, (patient_id, doctor_id, date_time, reason))

    db.commit()
    db.close()
    print("✔ 3 appointments added")


def report_appointments():
    db = connect_db()
    cursor = db.cursor()

    sql = """
    SELECT p.patient_id, p.full_name, p.date_of_birth, p.gender, p.address,
           d.full_name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """

    cursor.execute(sql)
    rows = cursor.fetchall()

    print("\nNo | Patient Name | Birthday | Gender | Address | Doctor | Reason | Date")
    for i, row in enumerate(rows, start=1):
        print(f"{i} | {row[1]} | {row[2]} | {row[3]} | {row[4]} | {row[5]} | {row[6]} | {row[7]}")

    db.close()


def appointments_today():
    db = connect_db()
    cursor = db.cursor()

    sql = """
    SELECT p.address, p.patient_id, p.full_name, p.date_of_birth, p.gender,
           d.full_name, a.status, a.reason
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = CURDATE()
    """

    cursor.execute(sql)
    rows = cursor.fetchall()

    print("\nAddress | No | Patient | Birthday | Gender | Doctor | Status | Note")
    for row in rows:
        print(f"{row[0]} | {row[1]} | {row[2]} | {row[3]} | {row[4]} | {row[5]} | {row[6]} | {row[7]}")

    db.close()

def main():
    while True:
        print("""
1. Add patients
2. Add doctors
3. Add appointments
4. Report appointments
5. Appointments today
0. Exit
""")
        choice = input("Choose: ")

        if choice == "1":
            add_patients()
        elif choice == "2":
            add_doctors()
        elif choice == "3":
            add_appointments()
        elif choice == "4":
            report_appointments()
        elif choice == "5":
            appointments_today()
        elif choice == "0":
            break

def create_database():
    conn = connect_server()
    cursor = conn.cursor()
    cursor.execute("CREATE DATABASE IF NOT EXISTS medical_service")
    cursor.execute("USE medical_service")

    cursor.execute("""
    CREATE TABLE IF NOT EXISTS patients (
        patient_id INT AUTO_INCREMENT PRIMARY KEY,
        full_name VARCHAR(255) NOT NULL,
        date_of_birth DATE NOT NULL,
        gender VARCHAR(10) NOT NULL,
        address VARCHAR(255),
        phone_number VARCHAR(15),
        email VARCHAR(100)
    )
    """)

    cursor.execute("""
    CREATE TABLE IF NOT EXISTS doctors (
        doctor_id INT AUTO_INCREMENT PRIMARY KEY,
        full_name VARCHAR(255) NOT NULL,
        specialization VARCHAR(100) NOT NULL,
        phone_number VARCHAR(15),
        email VARCHAR(100),
        years_of_experience INT
    )
    """)

    cursor.execute("""
    CREATE TABLE IF NOT EXISTS appointments (
        appointment_id INT AUTO_INCREMENT PRIMARY KEY,
        patient_id INT NOT NULL,
        doctor_id INT NOT NULL,
        appointment_date DATETIME NOT NULL,
        reason VARCHAR(255),
        status VARCHAR(50) DEFAULT 'pending',
        CONSTRAINT fk_patient_id FOREIGN KEY (patient_id) REFERENCES patients(patient_id),
        CONSTRAINT fk_doctor_id FOREIGN KEY (doctor_id) REFERENCES doctors(doctor_id)
    )
    """)

    conn.commit()
    conn.close()
    print("✔ Database and tables checked/created")

if __name__ == "__main__":
    create_database()
    main()
