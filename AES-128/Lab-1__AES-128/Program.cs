using System;

namespace Lab_1__AES_128
{
    class Program
    {
        static void Main(string[] args)
        {
            uint[,] Key = new uint[4,4]; //ключ ffeeddccbbaa99887766554433221100

            uint[,] State = new uint[4, 4]; ; //input 66554433221100ffe69385897adcbf93

            string KeyStr;
            string StateStr;

            bool flag = false;
            while (flag == false)
            {
                Console.Write("AES-128 \nЗашифровать(1), расшифровать(2) или выйти(3)?:  ");
                int num = Convert.ToInt32(Console.ReadLine());
                if (num == 1)
                {
                    bool flag1 = false;
                    while (flag1 == false)
                    {
                        Console.Write("Введите данные: ");
                        StateStr = Console.ReadLine();
                        if (StateStr.Length == 32)
                        {
                            flag1 = true;
                            State = ToArr(StateStr);
                        }
                        else
                        {
                            Console.WriteLine("Неправильная длина!!!");
                        }   
                    }

                    flag1 = false;
                    while (flag1 == false)
                    {
                        Console.Write("Введите ключ: ");
                        KeyStr = Console.ReadLine();
                        if (KeyStr.Length == 32)
                        {
                            flag1 = true;
                            Key = ToArr(KeyStr);
                        }
                        else
                        {
                            Console.WriteLine("Неправильная длина!!!");
                        }
                    }
                    
                    Console.Write("Результат: ");
                    Encrypt(State, Key);
                    Console.WriteLine();
                }
                else if (num == 2)
                {
                    bool flag1 = false;
                    while (flag1 == false)
                    {
                        Console.Write("Введите данные: ");
                        StateStr = Console.ReadLine();
                        if (StateStr.Length == 32)
                        {
                            flag1 = true;
                            State = ToArr(StateStr);
                        }
                        else
                        {
                            Console.WriteLine("Неправильная длина!!!");
                        }
                    }

                    flag1 = false;
                    while (flag1 == false)
                    {
                        Console.Write("Введите ключ: ");
                        KeyStr = Console.ReadLine();
                        if (KeyStr.Length == 32)
                        {
                            flag1 = true;
                            Key = ToArr(KeyStr);
                        }
                        else
                        {
                            Console.WriteLine("Неправильная длина!!!");
                        }
                    }
                    Console.Write("Результат: ");
                    Decrypt(State, Key);
                    Console.WriteLine();
                }
                else if (num == 3)
                {
                    flag = true;
                }
                else 
                {
                    Console.WriteLine("Такой команды нет!!!\n");
                }

            }

            
            Console.ReadKey();
        }


        static void Encrypt(uint[,] State, uint[,] Key)
        {
            uint[,] Rcon = {
        { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36},
        { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
        { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
        { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            };


            uint[,] KeyCopy = new uint[4, 4];
            uint[,] KeyCopy2 = new uint[4, 4];

            State = AddRoundKey(State, Key);



            for (int i = 0; i < 9; i++)
            {
                State = SubBytes(State);
                State = ShiftRows(State);
                State = MixColumns(State);

                for (int n = 0; n < 4; n++)
                    for (int z = 0; z < 4; z++)
                        KeyCopy[n, z] = Key[n, z];

                for (int n = 0; n < 4; n++)
                    for (int z = 0; z < 4; z++)
                        KeyCopy2[n, z] = Key[n, z];

                KeyCopy = RotWord(KeyCopy);
                KeyCopy = SubBytes(KeyCopy);
                for (int j = 0; j < 4; j++)
                {
                    Key[j, 0] = Key[j, 0] ^ KeyCopy[j, 3] ^ Rcon[j, i];
                }
                for (int j = 1; j < 4; j++)
                {
                    for (int n = 0; n < 4; n++)
                    {
                        Key[n, j] = KeyCopy2[n, j] ^ Key[n, j - 1];
                    }
                }


                State = AddRoundKey(State, Key);
            }

            State = SubBytes(State);
            State = ShiftRows(State);

            for (int n = 0; n < 4; n++)
                for (int z = 0; z < 4; z++)
                    KeyCopy[n, z] = Key[n, z];
            for (int n = 0; n < 4; n++)
                for (int z = 0; z < 4; z++)
                    KeyCopy2[n, z] = Key[n, z];

            KeyCopy = RotWord(KeyCopy);
            KeyCopy = SubBytes(KeyCopy);
            for (int j = 0; j < 4; j++)
            {
                Key[j, 0] = Key[j, 0] ^ KeyCopy[j, 3] ^ Rcon[j, 9];
            }
            for (int j = 1; j < 4; j++)
            {
                for (int n = 0; n < 4; n++)
                {
                    Key[n, j] = KeyCopy2[n, j] ^ Key[n, j - 1];
                }
            }

            State = AddRoundKey(State, Key);


            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Console.Write(Convert.ToString(State[j, i], 16));
            Console.WriteLine();
        }

        static void Decrypt(uint[,] State, uint[,] Key)
        {
            uint[,] Rcon = {
        { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36},
        { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
        { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
        { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            };

            uint[,,] KeyMas = new uint[11, 4, 4];
            uint[,] KeyCopy = new uint[4, 4];
            uint[,] KeyCopy2 = new uint[4, 4];

            for (int n = 0; n < 4; n++)
                for (int z = 0; z < 4; z++)
                    KeyMas[0, n, z] = Key[n, z];


            for (int i = 0; i < 10; i++)
            {
                for (int n = 0; n < 4; n++)
                    for (int z = 0; z < 4; z++)
                        KeyCopy[n, z] = Key[n, z];

                for (int n = 0; n < 4; n++)
                    for (int z = 0; z < 4; z++)
                        KeyCopy2[n, z] = Key[n, z];

                KeyCopy = RotWord(KeyCopy);
                KeyCopy = SubBytes(KeyCopy);
                for (int j = 0; j < 4; j++)
                {
                    Key[j, 0] = Key[j, 0] ^ KeyCopy[j, 3] ^ Rcon[j, i];
                }
                for (int j = 1; j < 4; j++)
                {
                    for (int n = 0; n < 4; n++)
                    {
                        Key[n, j] = KeyCopy2[n, j] ^ Key[n, j - 1];
                    }
                }

                for (int n = 0; n < 4; n++)
                    for (int z = 0; z < 4; z++)
                        KeyMas[i+1, n, z] = Key[n, z];
            }


            for (int n = 0; n < 4; n++)
                for (int z = 0; z < 4; z++)
                     Key[n, z] = KeyMas[10, n, z];


            State = AddRoundKey(State, Key);


            for (int i = 0; i < 9; i++)
            {
                uint[,] KeyCopy3 = new uint[4, 4];

                for (int n = 0; n < 4; n++)
                    for (int z = 0; z < 4; z++)
                        KeyCopy3[n, z] = KeyMas[9-i, n, z];

                State = InvShiftRows(State);
                State = InvSubBytes(State);

                

                State = AddRoundKey(State, KeyCopy3);
                State = InvMixColumns(State);
            }

            State = InvShiftRows(State);
            State = InvSubBytes(State);

            for (int n = 0; n < 4; n++)
                for (int z = 0; z < 4; z++)
                    Key[n, z] = KeyMas[0, n, z];

            State = AddRoundKey(State, Key);


            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Console.Write(Convert.ToString(State[j, i], 16));
            Console.WriteLine();
        }

        static uint[,] ToArr(string key)     // KeySchedule[r][c] = SecretKey[r + 4c], r = 0,1...4; c = 0,1..Nk.    Но т.к. в одну ячейку нужн 2 знака, то домнажаем все на 2
        {
            uint[,] keys = new uint[4,4];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    string val = "";
                    val = val + key[2*i+8*j] + key[2*i + 8*j+1];
                    keys[i, j] = Convert.ToUInt32(val, 16);
                }
            
            return keys;
        }

        static uint[,] SubBytes(uint[,] State)
        {
            uint[,] Sbox = {{0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76 },
        { 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0 },
        { 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15 },
        { 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75 },
        { 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84 },
        { 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf },
        { 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8 },
        { 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2 },
        { 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73 },
        { 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb },
        { 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79 },
        { 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08 },
        { 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a },
        { 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e },
        { 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf },
        { 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 } };

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    uint x = State[i, j] & 0b00001111;
                    uint y = (State[i, j] & 0b11110000)>>4;
                    State[i, j] = Sbox[y, x];
                }

            return State;
        }


        static uint[,] ShiftRows(uint[,] State)
        {
            uint val = State[1, 0];
            State[1, 0] = State[1, 1];
            State[1, 1] = State[1, 2];
            State[1, 2] = State[1, 3];
            State[1, 3] = val;

            val = State[2, 0];
            uint val2 = State[2, 1];
            State[2, 0] = State[2, 2];
            State[2, 2] = val;
            State[2, 1] = State[2, 3];
            State[2, 3] = val2;
            

            val = State[3, 0];
            State[3, 0] = State[3, 3];
            State[3, 3] = State[3, 2];
            State[3, 2] = State[3, 1];
            State[3, 1] = val;

            return State;
        }

        static uint[,] MixColumns(uint[,] State)  //процедура умножения 4 байтов столбца блока состояния (State) на фиксированный многочлен c(x)=3x³+x²+x+2 по модулю x^4+1 в поле Галуа. 
        {
            uint[,] StateCopy1 = new uint[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    StateCopy1[i, j] = State[i, j];

            uint[,] StateCopy2 = new uint[4,4];
            uint[,] pln =
            {
                {0x02, 0x03, 0x01, 0x01},
                {0x01, 0x02, 0x03, 0x01},
                {0x01, 0x01, 0x02, 0x03},
                {0x03, 0x01, 0x01, 0x02}
            };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    uint val = 0x00;
                    for (int n=0; n<4; n++)
                    {
                        StateCopy2[n, i] = StateCopy1[n, i];
                        if (pln[j, n] == 0x02 || pln[j, n] == 0x03)
                        {
                            
                            if (StateCopy1[n, i] < 0x80)
                            {
                                StateCopy2[n, i] = StateCopy2[n, i] << 1;
                            }
                            else
                            {
                                StateCopy2[n, i] = StateCopy2[n, i] << 1;
                                StateCopy2[n, i] = StateCopy2[n, i] ^ 0x1b;
                                StateCopy2[n, i] = StateCopy2[n, i] ^ 0x100;
                            }

                            if (pln[j, n] == 0x03)
                            {
                                StateCopy2[n, i] = StateCopy2[n, i] ^ StateCopy1[n, i];
                            }
                        }
                        else
                        {
                            StateCopy2[n, i] = StateCopy1[n, i];
                        }

                        val = val ^ StateCopy2[n, i];
                    }
                    State[j, i] = val;
                }
            }

            return State;
        }

        static uint[,] AddRoundKey(uint[,] State, uint[,] Key)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    State[j, i] = Key[j, i] ^ State[j, i];
                }
            }

            return State;
        }

        static uint[,] RotWord(uint[,] Key)
        {
            uint val = Key[0, 3];
            Key[0, 3] = Key[1, 3];
            Key[1, 3] = Key[2, 3];
            Key[2, 3] = Key[3, 3];
            Key[3, 3] = val;

            return Key;
        }

        static uint[,] InvSubBytes(uint[,] State)
        {
            uint[,] InvSbox = {{0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb },
        { 0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb },
        { 0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e },
        { 0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25 },
        { 0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92 },
        { 0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84 },
        { 0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06 },
        { 0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b },
        { 0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73 },
        { 0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e },
        { 0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b },
        { 0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4 },
        { 0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f },
        { 0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef },
        { 0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61 },
        { 0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d } };

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    uint x = State[i, j] & 0b00001111;
                    uint y = (State[i, j] & 0b11110000) >> 4;
                    State[i, j] = InvSbox[y, x];
                }

            return State;

        }

        static uint[,] InvShiftRows(uint[,] State)
        {
            uint val = State[1, 3];
            State[1, 3] = State[1, 2];
            State[1, 2] = State[1, 1];
            State[1, 1] = State[1, 0];
            State[1, 0] = val;

            val = State[2, 0];
            uint val2 = State[2, 1];
            State[2, 0] = State[2, 2];
            State[2, 2] = val;
            State[2, 1] = State[2, 3];
            State[2, 3] = val2;


            val = State[3, 0];
            State[3, 0] = State[3, 1];
            State[3, 1] = State[3, 2];
            State[3, 2] = State[3, 3];
            State[3, 3] = val;

            return State;
        }

        static uint m02(uint value)
        {
            if (value < 0x80)
            {
                value = value << 1;
            }
            else
            {
                value = value << 1;
                value = value ^ 0x1b;
                value = value ^ 0x100;
            }
            return value;
        }

        static uint[,] InvMixColumns(uint[,] State)  //процедура умножения 4 байтов столбца блока состояния (State) на фиксированный многочлен c(x)=3x³+x²+x+2 по модулю x^4+1 в поле Галуа. 
        {
            uint[,] StateCopy1 = new uint[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    StateCopy1[i, j] = State[i, j];

            uint[,] StateCopy2 = new uint[4, 4];
            uint[,] pln =
            {
                {0x0e, 0x0b, 0x0d, 0x09},
                {0x09, 0x0e, 0x0b, 0x0d},
                {0x0d, 0x09, 0x0e, 0x0b},
                {0x0b, 0x0d, 0x09, 0x0e}
            };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    uint val = 0x00;
                    for (int n = 0; n < 4; n++)
                    {
                        StateCopy2[n, i] = StateCopy1[n, i];
   

                        if (pln[j, n] == 0x09)
                        {
                            StateCopy2[n, i] = m02(StateCopy2[n,i]);
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = StateCopy2[n, i] ^ StateCopy1[n,i];
                        }
                        else if (pln[j, n] == 0x0b)
                        {
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = StateCopy2[n, i] ^ StateCopy1[n, i] ^ m02(StateCopy1[n, i]);
                        }
                        else if (pln[j, n] == 0x0d)
                        {
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = StateCopy2[n, i] ^ StateCopy1[n, i] ^ m02(m02(StateCopy1[n, i]));
                        }
                        else if (pln[j, n] == 0x0e)
                        {
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = m02(StateCopy2[n, i]);
                            StateCopy2[n, i] = StateCopy2[n, i] ^ m02(StateCopy1[n, i]) ^ m02(m02(StateCopy1[n, i]));
                        }

                        val = val ^ StateCopy2[n, i];
                    }
                    State[j, i] = val;
                }
            }
            return State;
        }
    }
}
