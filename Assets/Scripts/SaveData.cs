[System.Serializable]
public class SaveData
{
    public long royalJelly;             // ȯ��(�к�) ��ȭ
    public long honey;                  // �⺻ ��ȭ

    public float temperature;           // �µ�

    public long queenHealth;            // ����(Ŭ��Ŀ) ü���� ������ �� �ָ� ���ư��� ���� ���� ��� ����
    public long queenHealthPrice;       // ����(Ŭ��Ŀ) ü���� ��ȭ ���
    public long queenStorage;           // ����(Ŭ��Ŀ) ���ָӴ�(�������)�� ������ ���� �� ���� �����ؼ� ��� ����
    public long queenStoragePrice;      // ����(Ŭ��Ŀ) ���ָӴ�(�������) ��ȭ ���

    public long beeHealthPrice;         // �Ϲ�(����) ü���� ��ȭ ���
    public long beeStoragePrice;        // �Ϲ�(����) ���ָӴ�(�������) ��ȭ ���
    public long beeSpeedPrice;          // �Ϲ�(����) ���ָӴ�(�������) ��ȭ ���

    public long beeCount;               // �ܹ�(����) ���� ��
    public long beeCountPrice;          // �ܹ�(����) ���� ����
    public long honeyComb;              // �ܹ��� ���� �� �ִ� ����(����)
    public long honeyCombPrice;         // �ܹ��� ���� �� �ִ� ����(����) ���� ����

    public long royalQueenHealth;       // ����(Ŭ��Ŀ) ü���� ������ �� �ָ� ���ư��� ���� ���� ��� ����
    public long royalQueenHealthPrice;  // ����(Ŭ��Ŀ) ü���� ��ȭ ���
    public long royalQueenStorage;      // ����(Ŭ��Ŀ) ���ָӴ�(�������)�� ������ ���� �� ���� �����ؼ� ��� ����
    public long royalQueenStoragePrice; // ����(Ŭ��Ŀ) ���ָӴ�(�������) ��ȭ ���
    public long royalBeeHealthPrice;    // �Ϲ�(����) ü���� ��ȭ ���
    public long royalBeeStoragePrice;   // �Ϲ�(����) ���ָӴ�(�������) ��ȭ ���

    public long beeHealth;              // �ܹ�(����) ü���� ������ �� �ָ� ���ư��� ���� ���� ��� ����
    public long beeStorage;             // �ܹ�(����) ��������� ������ ���� �� ���� �����ؼ� ��� ����
    public long beeSpeed;               // �ܹ�(����) �ӵ��� ������ ���� �� ���� ������

    public long royalBeeHealth;         // �ܹ�(����) ü���� ������ �� �ָ� ���ư��� ���� ���� ��� ����
    public long royalBeeStorage;        // �ܹ�(����) ��������� ������ ���� �� ���� �����ؼ� ��� ����

    public string Queen;
    public string Bee;
    public string Map;

    public bool[] mapSkinList = { true, false, false, false, false, false};

    public bool[] queenSkinList = { true, false, false, false, false, false, false, false, false, false, false };

    public bool[] beeSkinList = { true, false, false, false, false, false, false, false, false, false, false };
}
