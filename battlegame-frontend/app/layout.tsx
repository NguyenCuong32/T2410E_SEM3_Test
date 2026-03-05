import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "BattleGame Report",
  description: "Player asset report for BattleGame",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}
