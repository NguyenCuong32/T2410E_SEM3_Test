from datetime import datetime

def generate_report(conn):
    cursor = conn.cursor()
    cursor.execute("""
        SELECT p.full_name, p.date_of_birth, p.gender, p.address, d.full_name, a.reason, a.appointment_date 
        FROM appointments a
        JOIN patients p ON a.patient_id = p.patient_id
        JOIN doctors d ON a.doctor_id = d.doctor_id
    """)
    results = cursor.fetchall()
    
    print("\n" + "="*110)
    print(f"{'No':<5} {'Patient name':<20} {'Birthday':<12} {'Gender':<8} {'Address':<15} {'Doctor name':<20} {'Reason':<20} {'Date':<20}")
    print("-" * 110)
    
    for i, row in enumerate(results, 1):
        d_str = row[6].strftime('%Y-%m-%d') if row[6] else ""
        print(f"{i:<5} {row[0]:<20} {str(row[1]):<12} {row[2]:<8} {row[3]:<15} {row[4]:<20} {row[5]:<20} {d_str:<20}")
    print("="*110)

def get_appointments_today(conn):
    cursor = conn.cursor()
    today = datetime.now().strftime('%Y-%m-%d')
    # SQL Server: CAST(GETDATE() AS DATE)
    cursor.execute("""
        SELECT p.address, a.appointment_id, p.full_name, p.date_of_birth, p.gender, d.full_name, a.status, a.reason
        FROM appointments a
        JOIN patients p ON a.patient_id = p.patient_id
        JOIN doctors d ON a.doctor_id = d.doctor_id
        WHERE CAST(a.appointment_date AS DATE) = CAST(GETDATE() AS DATE)
    """)
    results = cursor.fetchall()

    print("\n" + "="*110)
    print(f"REPORT: APPOINTMENTS TODAY ({today})")
    print("-" * 110)
    print(f"{'Address':<15} {'No':<5} {'Patient name':<20} {'Birthday':<12} {'Gender':<8} {'Doctor name':<20} {'Status':<10} {'Note':<20}")
    
    if not results:
        print("Không có lịch hẹn nào hôm nay.")
    else:
        for row in results:
            print(f"{row[0]:<15} {row[1]:<5} {row[2]:<20} {str(row[3]):<12} {row[4]:<8} {row[5]:<20} {row[6]:<10} {row[7]:<20}")
    print("="*110)