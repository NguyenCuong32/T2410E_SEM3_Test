from mysql.connector import Error

def add_patient(conn,full_name,dob,gender,address,phone,email):
    sql="INSERT INTO patients (full_name, date_of_birth, gender, address, phone_number, email) VALUES (%s, %s, %s, %s, %s, %s)"
    cursor=conn.cursor()
    cursor.execute(sql,(full_name,dob,gender,address,phone,email))
    conn.commit()
    return cursor.lastrowid

def add_doctor(conn, full_name, specialization, phone, email, experience):
    sql = "INSERT INTO doctors (full_name, specialization, phone_number, email, years_of_experience) VALUES (%s, %s, %s, %s, %s)"
    cursor = conn.cursor()
    cursor.execute(sql, (full_name, specialization, phone, email, experience))
    conn.commit()
    return cursor.lastrowid

def add_appointment(conn, patient_id, doctor_id, date, reason):
    sql = "INSERT INTO appointments (patient_id, doctor_id, appointment_date, reason, status) VALUES (%s, %s, %s, %s, 'Pending')"
    cursor = conn.cursor()
    cursor.execute(sql, (patient_id, doctor_id, date, reason))
    conn.commit()

def get_general_report(conn):
    sql = """
    SELECT p.full_name, p.date_of_birth, p.gender, p.address, d.full_name, a.reason, a.appointment_date 
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    ORDER BY a.appointment_date DESC
    """
    cursor = conn.cursor()
    cursor.execute(sql)
    return cursor.fetchall()

def get_today_apppointment(conn):
    sql="""
    SELECT p.address,p.full_name,p.phone_number,p.date_of_birth,d.full_name,a.status,a.reason
    FROM appointments a
    JOIN patients p ON a.patient_id = p.patient_id
    JOIN doctors d ON a.doctor_id = d.doctor_id
    WHERE DATE(a.appointment_date) = CURDATE()"""
    cursor=conn.cursor()
    cursor.execute(sql)
    return cursor.fetchall()