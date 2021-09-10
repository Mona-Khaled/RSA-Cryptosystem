    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;    
    using System.Numerics;

    namespace RSA
    {
        class Program
        {
            static string add(string number1, string number2)
            {
                StringBuilder result = new StringBuilder();
                //string result = "";
                int carry = 0;
                // if num1 is longer than num2 swap them
                // num2 should be the longest string
                if (number1.Length > number2.Length)
                {
                    string temp = number1;
                    number1 = number2;
                    number2 = temp;
                }
                int num1_len = number1.Length;
                int num2_len = number2.Length;
                int diff_len = num2_len - num1_len;
                //step1 add numbers from mun1 and num2 till num1 length which is the shortest one
                for (int i = num1_len - 1; i >= 0; i--)
                {
                    int sum = ((int)(number1[i] - '0') + (int)(number2[i + diff_len] - '0') + carry);
                    result.Append((char)(sum % 10 + '0'));
                    carry = sum / 10;
                }
                //step2 add the rest numbers of num2 which is the longest one
                for (int i = diff_len - 1; i >= 0; i--)
                {
                    int sum = ((int)(number2[i] - '0') + carry);
                    result.Append((char)(sum % 10 + '0'));
                    carry = sum / 10;
                }
                //step3 add carry
                if (carry > 0)
                {
                    result.Append( (char)(carry + '0'));
                }
                //reverse result string
                string result_builder = result.ToString();//tostring>>>>>O(N)
                char[] newresult = result_builder.ToCharArray();
                Array.Reverse(newresult);
                StringBuilder final_result = new StringBuilder();
                for (int i = 0; i < newresult.Length; i++)//llength>>>>O(1)
                {
                    if (newresult[i] == '0')
                    {
                        newresult[i] = '$';
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 0; i < newresult.Length; i++)
                {
                    if (newresult[i] != '$')
                    {
                        final_result.Append(newresult[i]);//append>>>>>O(1)
                    }
                }
                return final_result.ToString();//tostring>>>>>O(N),return>>>>>O(1)
                    //return new string(newresult);
                //return result;
            }
            static string sub(string number1, string number2)
            {
                StringBuilder result = new StringBuilder();
                // x and y holds the values of number1 and number2 as integers  
                int x = 0;
                Int32.TryParse(number1, out x);
                int y = 0;
                Int32.TryParse(number2, out y);
                //if both strings are equal in value
                if (number1 == number2)
                {
                    return ("0");
                }
                //if both strings are different then check lentgh and value
                else
                {
                    //if num2 is longer than num1 then swap
                    // num1 should be the longest one
                    if (number2.Length > number1.Length)
                    {
                        string temp = number2;
                        number2 = number1;
                        number1 = temp;
                    }
                    //if number2 is larger than number1 then swap them
                    else if (number1.Length == number2.Length)
                    {
                        if (y > x)
                        {
                            int temp = y;
                            y = x;
                            x = temp;
                        }
                        //number1 = x.ToString();
                        //number2 = y.ToString();
                    }
                    int num1_len = number1.Length;//O(1)
                    int num2_len = number2.Length;//O(1)
                    int diff_len = num1_len - num2_len;
                    int borrow = 0;
                    char[] arr = new char[1000];
                    //step1 subtract num1 from num2 until the length of num2 which is the shortest
                    for (int i = num2_len - 1; i >= 0; i--)
                    {
                        int diff = (((int)number1[i + diff_len] - (int)'0') - ((int)number2[i] - (int)'0') - borrow);
                        if (diff < 0)
                        {
                            diff += 10;
                            borrow = 1;
                        }
                        else
                        {
                            borrow = 0;
                        }
                        arr[i] += (char)diff;
                        //result.Append(diff.ToString());
                    }
                    result.Append(arr.ToString());
                    //step2 accumalate rest bits from num1 which is the longest one to the result
                    for (int i = diff_len - 1; i >= 0; i--)
                    {
                        if (number1[i] == '0' && borrow > 0)
                        {
                            result.Append("9");
                            continue;
                        }
                        int diff = (((int)number1[i] - (int)'0') - borrow);
                        if (i > 0 || diff > 0)
                        {

                            //result.Append(diff.ToString());
                            arr[i] += (char)diff;
                        }
                        borrow = 0;
                    }
                    result.Append(arr.ToString());
                    string result_builder = result.ToString();
                    char[] newresult = result_builder.ToCharArray();
                    Array.Reverse(newresult);
                    StringBuilder final_result = new StringBuilder();
                    for (int i = 0; i < newresult.Length; i++)
                    {
                        if (newresult[i] == '0')
                        {
                            newresult[i] = '$';
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i < newresult.Length; i++)
                    {
                        if (newresult[i] != '$')
                        {
                            final_result.Append(newresult[i]);
                        }
                    }
                    return final_result.ToString();
                    //return new string(newresult);
                    //return result;
                }
            }
            // a uility function to multiply a single bits of str1 and str2
            static int MulSingleBit(string number1, string number2)
            {
                return (char)(number1[0] - '0') * (char)(number2[0] - '0');
            }
            static int equallystrings(ref string number1,ref string number2)
            {
                int num1_length = number1.Length;  //O(1)
                int num2_length = number2.Length;   //O(1)
                //both strings must be equal in length
                if (num1_length > num2_length)  //O(1)
                {
                    number2 = number2.PadLeft(num1_length, '0');  //O(1) //shift the string from left and put 0's according to the biggest lenght
                    return number2.Length;  //O(1)
                }
                else if (num1_length < num2_length)  //O(1)
                    number1 = number1.PadLeft(num2_length, '0');  //O(1)
                return number1.Length;  //O(1) //return the number1Lenght when length1 >= length2

            }
          static public string mul(string number1, string number2)
            {
                //function that return the length after equalling them
                int num_length = equallystrings(ref number1,ref number2); //O(1)
                int mid = num_length / 2;  //O(1)
                //base case
                if (num_length == 1) //O(1)
                {
                    return (((number1[0]) - '0') * ((number2[0]) - '0')).ToString();  //O(1)
                }
                else if (num_length == 0) //O(1)
                {
                    return "0";
                }
                //divide step
                string a = number1.Substring(0, mid);  //O(N)
                string b = number1.Substring(mid);  //O(N)
                string c = number2.Substring(0, mid);  //O(N)
                string d = number2.Substring(mid);  //O(N)
                //conquer step
                string ac = mul(a, c);  //recursive step //T(N/2)
                string bd = mul(b, d);  //recursive step //T(N/2)
                string z = mul(add(a, b), add(c, d));  //recursive step //T(N/2)
                string firstTerm = add(ac, bd);  //O(N)
                string remainingTerm = sub(z, firstTerm);  //O(N)
                //combine step
                string ans = add(ac.PadRight((ac.Length + (num_length - mid) * 2), '0'), remainingTerm.PadRight((remainingTerm.Length + (num_length - mid)), '0')); //O(N)
                string finalAns = add(ans, bd); //O(N)
                return finalAns;
            }
            static string mymul1(string number1, string number2)
            {
                string result = "";
                int num1_length = number1.Length;
                int num2_length = number2.Length;
                //both strings must be equal in length
                if (num1_length > num2_length)
                {
                    int diff_length = num1_length - num2_length;
                    string temp = "";
                    for (int i = 0; i <= diff_length; i++)
                    {
                        temp += '0';
                    }
                    temp += number2;
                    number2 = temp;
                }
                else if (num1_length < num2_length)
                {
                    int diff_length = num2_length - num1_length;
                    string temp = "";
                    for (int i = 0; i <= diff_length; i++)
                    {
                        temp += '0';
                    }
                    temp += number1;
                    number1 = temp;
                }
                //divide
                    string first_half_num1 = "";//a
                    string second_half_num1 = "";//b
                    string first_half_num2 = "";//c
                    string second_half_num2 = "";//d
                    //divide number1 to two strings a=first_half_num1, b=second_half_num1
                    first_half_num1 = number1.Substring(0, num1_length / 2);
                    second_half_num1 = number1.Substring((num1_length / 2) + 1, num1_length);
                    //divide number2 to two strings c=first_half_num2, d=second_half_num2
                    first_half_num2 = number2.Substring(0, num2_length / 2);
                    second_half_num2 = number2.Substring((num2_length / 2) + 1, num2_length);
                //base case
                    if (number1.Length == 0)
                    {
                        return (0).ToString();
                    }
                    if (number1.Length == 1)
                    {
                        return (MulSingleBit(number1, number2)).ToString();
                    }
                //recursively calculate three products
                    string p1 = mymul1(first_half_num1, first_half_num2);//a*c
                    string p2 = mymul1(second_half_num1, second_half_num2);//b*d
                    string p3 = mymul1(add(first_half_num1, second_half_num1), add(first_half_num2, second_half_num2));
                //combine three products to get final result
                    string sum_p1_p2 = add(p1, p2);
                    string subtract_p3_from_p1_p2= sub(p3, sum_p1_p2);
                //add zeros to p2
                    string temp_p2 = "";
                    for (int i = 0; i < p1.Length; i++)
                    {
                        temp_p2 += '0';
                    }
                    temp_p2 += p2;
                    p2 = temp_p2;
                //add zeros to subtraction p3 from p1,p2
                    string temp_sub = "";
                    for (int i = 0; i < p1.Length; i++)
                    {
                        temp_sub += '0';
                    }
                    temp_sub += subtract_p3_from_p1_p2;
                    subtract_p3_from_p1_p2 = temp_sub;
                //add p1+p2+((p1+p2)-p3)
                    string pre_result = add(p2, subtract_p3_from_p1_p2);
                    result = add(pre_result, p1);
                    return result;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            static string mymul2(string number1, string number2)
            {
                string result = "";
                //int product = 0;
                if (number2.Length > number1.Length)
                {
                    string temp = "";
                    temp = number2;
                    number2 = number1;
                    number1 = temp;
                }
                string[] num2_arr = new string[number2.Length];
                int zero_counter = 0;
                for (int i = number2.Length - 1; i >= 0; i--)
                {
                   // num2_arr[i] = number2[i] + (char)zero_counter;
                    zero_counter++;
                }
                    return result;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            static string div(string number1, string number2)
            {
                //number2 is the divisor "number1/number2"
                StringBuilder result = new StringBuilder();
                int divisor = 0;
                int counter = 0;
                Int32.TryParse(number2, out divisor);
                int temp = (int)(number1[counter] - '0');
                while (temp < divisor)
                {
                    temp = temp * 10 + (int)(number1[counter + 1] - '0');
                    counter++;
                }
                counter++;
                while (number1.Length > counter)
                {
                    result.Append((char)(temp / divisor + '0'));
                    temp = (temp % divisor) * 10 + (int)(number1[counter] - '0');
                    counter++;
                }
                result.Append((char)(temp / divisor + '0'));
                if (result.Length == 0)
                {
                    return "0";
                }
                return result.ToString();
            }
            static string mod_of_power(string number1, string number2)
            {

                return "";
            }



            static void Main(string[] args)
            {
                Big_Integer b = new Big_Integer();
            List<int> list = new List<int>();
            

            int k = 4; // Number of iterations 
            int e = 0;
            string n = "";
            Console.WriteLine("All primes smaller " +
                                       "than 100: ");

            for (int n = 2; n < 10000; n++)
            {
                if (b.isPrime(n, k))
                    Console.Write(n + " ");
                list.Add(n);
            }
            int listCount = list.Count();
            Random r1 = new Random();
              int q=r1.Next(list[0], listCount);
            Random r2 = new Random();
             int p = r2.Next(list[0], listCount);
            b.public_key(e,n,q,p);
            //string result = " ";
            //string x = Console.ReadLine();
            //string y = Console.ReadLine();
            //Big_Integer.div bi = b.mydiv(x, y);
            //x=bi.qoutient;
            //y=bi.remainder;
            //Console.WriteLine(x);
            //Console.WriteLine(y);

            // //////////FOR ADD FUNCTION///////////////
            //// read test cases for add function in a list
            // List<string> add_list = new List<string>();
            // StreamReader sr_add = new StreamReader("AddTestCases.txt");
            // string num_to_add = sr_add.ReadLine();
            // while (num_to_add != null)
            // {
            //     add_list.Add(num_to_add);
            //     num_to_add = sr_add.ReadLine();
            // }
            // sr_add.Close();
            // //create file and write the outputs in it "add outputs"
            // FileStream fs_add = new FileStream("Add-Output.txt", FileMode.OpenOrCreate);
            // StreamWriter sw_add = new StreamWriter(fs_add);
            // for (int i = 0; i < add_list.Count; i += 2)
            // {
            //     string result_add = b.add(add_list[i], add_list[i + 1]);
            //     sw_add.WriteLine(result_add);
            //     sw_add.WriteLine();
            // }
            // sw_add.Close();
            // fs_add.Close();
            // ////////////FOR SUB FUNCTION///////////////
            // //read test cases for sub function in a list
            // List<string> sub_list = new List<string>();
            // StreamReader sr_sub = new StreamReader("SubtractTestCases.txt");
            // string num_to_sub = sr_sub.ReadLine();
            // while (num_to_sub != null)
            // {
            //     sub_list.Add(num_to_sub);
            //     num_to_sub = sr_sub.ReadLine();
            // }
            // sr_add.Close();
            // //create file and write the outputs in it "sub outputs"
            // FileStream fs_sub = new FileStream("Sub-Output.txt", FileMode.OpenOrCreate);
            // StreamWriter sw_sub = new StreamWriter(fs_sub);
            // for (int i = 0; i < sub_list.Count; i += 2)
            // {
            //     string result_sub = b.sub(sub_list[i], sub_list[i + 1]);
            //     sw_sub.WriteLine(result_sub);
            //     sw_sub.WriteLine();
            // }
            // sw_sub.Close();
            // fs_sub.Close();
            // ////////////FOR MUL FUNCTION///////////////
            // //read test cases for mul function in a list
            // List<string> mul_list = new List<string>();
            // StreamReader sr_mul = new StreamReader("MultiplyTestCases.txt");
            // string num_to_mul = sr_mul.ReadLine();
            // while (num_to_mul != null)
            // {
            //     mul_list.Add(num_to_mul);
            //     num_to_mul = sr_mul.ReadLine();
            // }
            // sr_mul.Close();
            // //create file and write the outputs in it "mul outputs"
            // FileStream fs_mul = new FileStream("Mul-Output.txt", FileMode.OpenOrCreate);
            // StreamWriter sw_mul = new StreamWriter(fs_mul);
            // for (int i = 0; i < mul_list.Count; i += 2)
            // {
            //     string result_mul = b.mul(mul_list[i], mul_list[i + 1]);
            //     sw_mul.WriteLine(result_mul);
            //     sw_mul.WriteLine();
            // }
            // sw_mul.Close();
            // fs_mul.Close();
            // ////////////FOR DIV FUNCTION///////////////
            // //read test cases for mul function in a list
            // List<string> div_list = new List<string>();
            // StreamReader sr_div = new StreamReader("MultiplyTestCases.txt");
            // string num_to_div = sr_mul.ReadLine();
            // while (num_to_mul != null)
            // {
            //     mul_list.Add(num_to_mul);
            //     num_to_mul = sr_mul.ReadLine();
            // }
            // sr_mul.Close();
            // //create file and write the outputs in it "mul outputs"
            // FileStream fs_div = new FileStream("Mul-Output.txt", FileMode.OpenOrCreate);
            // StreamWriter sw_div = new StreamWriter(fs_mul);
            // for (int i = 0; i < mul_list.Count; i += 2)
            // {
            //     string result_mul = b.mul(mul_list[i], mul_list[i + 1]);
            //     sw_mul.WriteLine(result_mul);
            //     sw_mul.WriteLine();
            // }
            // sw_mul.Close();
            // fs_mul.Close();
            //string x = "12";
            //string y = "23";
            //string a = "1537983";
            //string b = "23606766456";
            //string x = mul(a, b);
            //x += y;
            //Console.WriteLine(x);
        }
    }
    }
