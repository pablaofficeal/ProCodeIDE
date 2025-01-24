#include <QApplication>
#include <QMainWindow>
#include <QTextEdit>
#include <QPushButton>
#include <QVBoxLayout>
#include <QMenuBar>
#include <QMenu>
#include <QAction>
#include <QProcess>
#include <QFileDialog>
#include <QMessageBox>

class ProCodeIDE : public QMainWindow {
    Q_OBJECT

public:
    ProCodeIDE() {
        // ����������� ������
        QWidget* centralWidget = new QWidget(this);

        // ��������� ��������
        editorTextBox = new QTextEdit(this);
        editorTextBox->setStyleSheet("background-color: #aefeff; font: bold 12pt 'Yu Gothic UI';");

        // ������ �������
        runButton = new QPushButton("Run", this);
        connect(runButton, &QPushButton::clicked, this, &ProCodeIDE::runCode);

        // ���� ������
        outputTextBox = new QTextEdit(this);
        outputTextBox->setReadOnly(true);
        outputTextBox->setStyleSheet("background-color: #d3d3d3;");

        // �����
        QVBoxLayout* layout = new QVBoxLayout();
        layout->addWidget(editorTextBox);
        layout->addWidget(runButton);
        layout->addWidget(outputTextBox);

        centralWidget->setLayout(layout);
        setCentralWidget(centralWidget);

        // ����
        QMenuBar* menuBar = new QMenuBar(this);
        QMenu* settingsMenu = new QMenu("���������", this);
        QMenu* themeMenu = new QMenu("����", this);

        QAction* blackThemeAction = new QAction("������", this);
        QAction* whiteThemeAction = new QAction("�����", this);

        connect(blackThemeAction, &QAction::triggered, this, &ProCodeIDE::changeToBlackTheme);
        connect(whiteThemeAction, &QAction::triggered, this, &ProCodeIDE::changeToWhiteTheme);

        themeMenu->addAction(blackThemeAction);
        themeMenu->addAction(whiteThemeAction);
        settingsMenu->addMenu(themeMenu);
        menuBar->addMenu(settingsMenu);

        setMenuBar(menuBar);

        // ��������� ��������� ����
        setWindowTitle("ProCode IDE");
        resize(800, 600);
    }

private slots:
    void runCode() {
        QString code = editorTextBox->toPlainText();

        // ��������� ���� ��� Python-����
        QString tempFile = QDir::tempPath() + "/temp_code.py";
        QFile file(tempFile);
        if (file.open(QIODevice::WriteOnly | QIODevice::Text)) {
            QTextStream out(&file);
            out << code;
            file.close();
        }

        // ���������� Python-����
        QProcess process;
        process.setProgram("python3"); // ������� ���� � Python, ���� ���������
        process.setArguments({ tempFile });
        process.start();
        process.waitForFinished();

        QString output = process.readAllStandardOutput();
        QString error = process.readAllStandardError();

        if (!error.isEmpty()) {
            outputTextBox->setText("Error: " + error);
        }
        else {
            outputTextBox->setText(output);
        }
    }

    void changeToBlackTheme() {
        setStyleSheet("background-color: black; color: white;");
        editorTextBox->setStyleSheet("background-color: black; color: white;");
        outputTextBox->setStyleSheet("background-color: black; color: white;");
    }

    void changeToWhiteTheme() {
        setStyleSheet("background-color: white; color: black;");
        editorTextBox->setStyleSheet("background-color: white; color: black;");
        outputTextBox->setStyleSheet("background-color: white; color: black;");
    }

private:
    QTextEdit* editorTextBox;
    QPushButton* runButton;
    QTextEdit* outputTextBox;
};

int main(int argc, char* argv[]) {
    QApplication app(argc, argv);

    ProCodeIDE ide;
    ide.show();

    return app.exec();
}
