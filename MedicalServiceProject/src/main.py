import db_connect
import data_entry
import reports

def main():
    conn = db_connect.get_connection()
    if not conn:
        return

    while True:
        print("\n--- QUẢN LÝ PHÒNG KHÁM ---")
        print("1. Nhập liệu (3 Bệnh nhân, 5 Bác sĩ) [Question 2]")
        print("2. Đặt lịch khám (3 Lịch) [Question 3]")
        print("3. Báo cáo tổng hợp [Question 4]")
        print("4. Lịch khám hôm nay [Question 5]")
        print("0. Thoát")
        
        choice = input("Chọn chức năng: ")
        
        if choice == '1':
            data_entry.input_data_batch(conn)
        elif choice == '2':
            data_entry.input_appointments_batch(conn)
        elif choice == '3':
            reports.generate_report(conn)
        elif choice == '4':
            reports.get_appointments_today(conn)
        elif choice == '0':
            conn.close()
            break
        else:
            print("Sai chức năng!")

if __name__ == "__main__":
    main()