# T2410E_SEM3_Test
1. (MySQL)
CREATE DATABASE medical_service;
USE medical_service;

-- Patient table
CREATE TABLE patients (
    patient_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100),
    birthday YEAR,
    gender VARCHAR(10),
    address VARCHAR(100)
);

-- Doctor table
CREATE TABLE doctors (
    doctor_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100),
    specialty VARCHAR(100)
);

-- Appointment table
CREATE TABLE appointments (
    appointment_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT,
    doctor_id INT,
    reason VARCHAR(200),
    appointment_date DATE,
    status VARCHAR(20),
    note VARCHAR(200),
    FOREIGN KEY (patient_id) REFERENCES patients(patient_id),
    FOREIGN KEY (doctor_id) REFERENCES doctors(doctor_id)
);

2. 

Cài thư viện:

pip install mysql-connector-python

import mysql.connector
from datetime import date

def connect_db():
    return mysql.connector.connect(
        host="localhost",
        user="root",
        password="123456",   # đổi cho đúng
        database="medical_service"
    )

3. 
def add_patients():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(3):
        print(f"\nEnter patient {i+1}:")
        name = input("Name: ")
        birthday = input("Birth year: ")
        gender = input("Gender: ")
        address = input("Address: ")

        sql = """
        INSERT INTO patients(name, birthday, gender, address)
        VALUES (%s, %s, %s, %s)
        """
        cursor.execute(sql, (name, birthday, gender, address))

    conn.commit()
    conn.close()

Thêm Doctor
def add_doctors():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(5):
        print(f"\nEnter doctor {i+1}:")
        name = input("Doctor name: ")
        specialty = input("Specialty: ")

        sql = "INSERT INTO doctors(name, specialty) VALUES (%s, %s)"
        cursor.execute(sql, (name, specialty))

    conn.commit()
    conn.close()

4. 
def add_appointments():
    conn = connect_db()
    cursor = conn.cursor()

    for i in range(3):
        print(f"\nEnter appointment {i+1}:")
        patient_id = int(input("Patient ID: "))
        doctor_id = int(input("Doctor ID: "))
        reason = input("Reason: ")
        app_date = input("Date (YYYY-MM-DD): ")
        status = input("Status (Pending/Done): ")
        note = input("Note: ")

        sql = """
        INSERT INTO appointments
        (patient_id, doctor_id, reason, appointment_date, status, note)
        VALUES (%s,%s,%s,%s,%s,%s)
        """
        cursor.execute(sql, (patient_id, doctor_id, reason, app_date, status, note))

    conn.commit()
    conn.close()

5. 
def report_all_appointments():
    conn = connect_db()
    cursor = conn.cursor()

    sql = """
    SELECT p.patient_id, p.name, p.birthday, p.gender, p.address,
           d.name, a.reason, a.appointment_date
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    """

    cursor.execute(sql)
    results = cursor.fetchall()

    print("\nNo | Patient Name | Birthday | Gender | Address | Doctor Name | Reason | Date")
    for r in results:
        print(f"{r[0]} | {r[1]} | {r[2]} | {r[3]} | {r[4]} | {r[5]} | {r[6]} | {r[7]}")

    conn.close()

6. 
def appointments_today():
    conn = connect_db()
    cursor = conn.cursor()

    today = date.today()

    sql = """
    SELECT p.address, p.patient_id, p.name, p.birthday, p.gender,
           d.name, a.status, a.note
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE a.appointment_date = %s
    """

    cursor.execute(sql, (today,))
    results = cursor.fetchall()

    print("\nAddress | No | Patient Name | Birthday | Gender | Doctor Name | Status | Note")
    for r in results:
        print(f"{r[0]} | {r[1]} | {r[2]} | {r[3]} | {r[4]} | {r[5]} | {r[6]} | {r[7]}")

    conn.close()

7. 
def main():
    add_patients()
    add_doctors()
    add_appointments()
    report_all_appointments()
    appointments_today()

if __name__ == "__main__":
    main()
