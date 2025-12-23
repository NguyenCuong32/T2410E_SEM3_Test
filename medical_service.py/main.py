from models.patient import add_patients
from models.doctor import add_doctors
from models.appointment import add_appointments
from reports.report import report, appointments_today


def menu():
    while True:
        print("""
========== MEDICAL SERVICE SYSTEM ==========
1. Thêm 3 bệnh nhân
2. Thêm 5 bác sĩ
3. Thêm 3 lịch hẹn
4. Báo cáo
5. Lịch hẹn hôm nay
0. Thoát
==========================================
        """)

        choice = input("Chọn chức năng: ")

        if choice == "1":
            add_patients()
        elif choice == "2":
            add_doctors()
        elif choice == "3":
            add_appointments()
        elif choice == "4":
            report()
        elif choice == "5":
            appointments_today()
        elif choice == "0":
            print("👋 Thoát chương trình")
            break
        else:
            print("❌ Lựa chọn không hợp lệ!")


if __name__ == "__main__":
    menu()
