using System;
using System.Drawing;
using System.Drawing.Drawing2D;

class Sort_visual
{
    Random rnd;

    class Box
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public int v;
        public Color clr = Color.SkyBlue;

        public Box(int x, int y, int width, int height, int v)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.v = v;
        }
        public Box(int x, int y, int width, int height, int v, Color clr)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.v = v;
            this.clr = clr;
        }
    }

    private int W = 1920, H = 1920, MAX_H = 500, MIN_H = 100, MAX_W = 100, diff_x = 500, diff_y = 150;

    public Sort_visual()
    {
        rnd = new Random();
    }

    private void save_frame(int f, ref Bitmap frame, ref Graphics g, ref Box[] boxes, int N, string str = "")
    {
        g.Clear(Color.White);
        Heap_Background(ref frame, ref g, N); //merge sort graphic
        for (int j = 0; j < N * 2; j++)
        {
            g.FillRectangle(new SolidBrush(boxes[j].clr), boxes[j].x, boxes[j].y, boxes[j].width, boxes[j].height);
            int offset_x = -5, offset_y = -80; //글자 위치
            if (boxes[j].v < 10)
            {
                offset_x = 20;
            }
            g.DrawString(boxes[j].v + "", new Font("나눔고딕", 50), new SolidBrush(Color.Black), boxes[j].x + offset_x, boxes[j].y + boxes[j].height + offset_y);
        }
        g.DrawString(str, new Font("나눔고딕", 50), new SolidBrush(Color.Orange), 50, 50);
        frame.Save("frames\\" + f + ".png", System.Drawing.Imaging.ImageFormat.Png);
    }

    private void color_transform(ref int f, ref Bitmap frame, ref Graphics g, ref Box[] boxes, int N, int j, Color clr, string str = "")
    {
        int c_r = boxes[j].clr.R, c_r_ = clr.R;
        int c_g = boxes[j].clr.G, c_g_ = clr.G;
        int c_b = boxes[j].clr.B, c_b_ = clr.B;

        int d = 20;
        for(int i = 0; i <= d; i++)
        {
            int r_ = (int)(c_r + (double)(c_r_ - c_r) * ((double)i / (double)d));
            int g_ = (int)(c_g + (double)(c_g_ - c_g) * ((double)i / (double)d));
            int b_ = (int)(c_b + (double)(c_b_ - c_b) * ((double)i / (double)d));
            boxes[j].clr = Color.FromArgb(255, r_, g_, b_);

            save_frame(f++, ref frame, ref g, ref boxes, N, str);
        }
    }

    private void color_transform(ref int f, ref Bitmap frame, ref Graphics g, ref Box[] boxes, int N, int[] j, Color[] clr, string str = "")
    {
        int cnt = j.Length;

        int[] c_r = new int[cnt], c_r_ = new int[cnt];
        int[] c_g = new int[cnt], c_g_ = new int[cnt];
        int[] c_b = new int[cnt], c_b_ = new int[cnt];

        for (int k = 0; k < cnt; k++)
        {
            c_r[k] = boxes[j[k]].clr.R;
            c_r_[k] = clr[k].R;
            c_g[k] = boxes[j[k]].clr.G;
            c_g_[k] = clr[k].G;
            c_b[k] = boxes[j[k]].clr.B;
            c_b_[k] = clr[k].B;
        }

        int d = 20;
        for (int i = 0; i <= d; i++)
        {
            for(int u = 0; u < cnt; u++)
            {
                int r_ = (int)(c_r[u] + (double)(c_r_[u] - c_r[u]) * ((double)i / (double)d));
                int g_ = (int)(c_g[u] + (double)(c_g_[u] - c_g[u]) * ((double)i / (double)d));
                int b_ = (int)(c_b[u] + (double)(c_b_[u] - c_b[u]) * ((double)i / (double)d));
                boxes[j[u]].clr = Color.FromArgb(255, r_, g_, b_);
            }

            save_frame(f++, ref frame, ref g, ref boxes, N, str);
        }
    }

    private void swap_transform(ref int f, ref Bitmap frame, ref Graphics g, ref Box[] boxes, int N, int j, int i, string str = "")
    {
        int p_x_j = boxes[j].x, p_x_i = boxes[i].x;
        int p_y_j = boxes[j].y, p_y_i = boxes[i].x;

        int d = 20;
        for(int k = 0; k <= d; k++)
        {
            int x_j = (int)(p_x_j + (double)(p_x_i - p_x_j) * ((double)k / (double)d));
            int x_i = (int)(p_x_i + (double)(p_x_j - p_x_i) * ((double)k / (double)d));
            int y_j = (int)(p_y_j + (double)(p_y_i - p_y_j) * ((double)k / (double)d));
            int y_i = (int)(p_y_i + (double)(p_y_j - p_y_i) * ((double)k / (double)d));
            boxes[j].x = x_j;
            boxes[i].x = x_i;

            save_frame(f++, ref frame, ref g, ref boxes, N, str);
        }
    }

    private void swap_transform(ref int f, ref Bitmap frame, ref Graphics g, ref Box[] boxes, int N, int[] j, int[] i, string str = "")
    {
        int cnt = j.Length;
        int[] p_x_j = new int[cnt], p_x_i = new int[cnt];
        int[] p_y_j = new int[cnt], p_y_i = new int[cnt];
        for(int k = 0; k < cnt; k++)
        {
            p_x_j[k] = boxes[j[k]].x;
            p_x_i[k] = boxes[i[k]].x;
            p_y_j[k] = boxes[j[k]].y;
            p_y_i[k] = boxes[i[k]].y;
        }

        int d = 20;
        for (int k = 0; k <= d; k++)
        {
            for(int u = 0; u < cnt; u++)
            {
                int x_j = (int)(p_x_j[u] + (double)(p_x_i[u] - p_x_j[u]) * ((double)k / (double)d));
                int x_i = (int)(p_x_i[u] + (double)(p_x_j[u] - p_x_i[u]) * ((double)k / (double)d));
                int y_j = (int)(p_y_j[u] + (double)(p_y_i[u] - p_y_j[u]) * ((double)k / (double)d));
                int y_i = (int)(p_y_i[u] + (double)(p_y_j[u] - p_y_i[u]) * ((double)k / (double)d));
                boxes[j[u]].x = x_j;
                boxes[i[u]].x = x_i;
                boxes[j[u]].y = y_j;
                boxes[i[u]].y = y_i;
            }
            
            save_frame(f++, ref frame, ref g, ref boxes, N, str);
        }
    }

    private void pos_transform(ref int f, ref Bitmap frame, ref Graphics g, ref Box[] boxes, int N, int j, int i, string str = "")
    {
        int p_x = boxes[j].x;
        int p_x_g = diff_x * (i + 1);

        int d = 20;
        for (int k = 0; k <= d; k++)
        {
            int x_j = (int)(p_x + (double)(p_x_g - p_x) * ((double)k / (double)d));
            boxes[j].x = x_j;

            save_frame(f++, ref frame, ref g, ref boxes, N, str);
        }
    }

    private void move_transform(ref int f, ref Bitmap frame, ref Graphics g, ref Box[] boxes, int N, int j, int x, int y, string str = "")
    {
        int p_x = boxes[j].x, p_y = boxes[j].y;

        int d = 20;
        for (int k = 0; k <= d; k++)
        {
            int x_j = (int)(p_x + (double)(x - p_x) * ((double)k / (double)d));
            int y_j = (int)(p_y + (double)(y - p_y) * ((double)k / (double)d));
            boxes[j].x = x_j;
            boxes[j].y = y_j;

            save_frame(f++, ref frame, ref g, ref boxes, N, str);
        }
    }

    private void swap_box(ref Box[] boxes, int j, int i)
    {
        Box tmp = new Box(boxes[j].x, boxes[j].y, boxes[j].width, boxes[j].height, boxes[j].v, boxes[j].clr);
        boxes[j] = new Box(boxes[i].x, boxes[i].y, boxes[i].width, boxes[i].height, boxes[i].v, boxes[i].clr);
        boxes[i] = new Box(tmp.x, tmp.y, tmp.width, tmp.height, tmp.v, tmp.clr);
    }

    public void Selection_Sort(ref int[] arr)
    {
        int f = 0;

        Bitmap frame = new Bitmap(W, H);
        Graphics g = Graphics.FromImage(frame);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.Clear(Color.White);

        int N = arr.Length;

        Box[] boxes = new Box[N];
        for(int j = 0; j < N; j++)
        {
            int height = (int)((double)(MAX_H - MIN_H) * ((double)arr[j] / (double)N)) + MIN_H;
            boxes[j] = new Box(diff_x * (j + 1), H - 100 - height, MAX_W, height, arr[j]);
        }

        for(int j = 0; j < 20; j++) save_frame(f++, ref frame, ref g, ref boxes, N);
        
        for (int j = 0; j < N - 1; j++)
        {
            int min = j;
            color_transform(ref f, ref frame, ref g, ref boxes, N, min, Color.LightSlateGray, j + " 인덱스 탐색");
            color_transform(ref f, ref frame, ref g, ref boxes, N, min, Color.OrangeRed, min + " 인덱스를 최솟값으로 선택");
            for (int i = j + 1; i < N; i++)
            {
                color_transform(ref f, ref frame, ref g, ref boxes, N, i, Color.LightSlateGray, i + " 인덱스 탐색");
                if (arr[min] > arr[i])
                {
                    color_transform(ref f, ref frame, ref g, ref boxes, N, min, Color.SkyBlue, i + " 인덱스를 최솟값으로 선택");
                    color_transform(ref f, ref frame, ref g, ref boxes, N, i, Color.OrangeRed, i + " 인덱스를 최솟값으로 선택");

                    min = i;
                }
                else
                {
                    color_transform(ref f, ref frame, ref g, ref boxes, N, i, Color.SkyBlue, i + " 인덱스 탐색");
                }
            }
            
            swap_transform(ref f, ref frame, ref g, ref boxes, N, j, min, "선택한 " + min + " 인덱스를 정렬");
            color_transform(ref f, ref frame, ref g, ref boxes, N, min, Color.Orange, "선택한 " + min + " 인덱스를 정렬");
            swap_box(ref boxes, j, min);
            Swap(ref arr, j, min);
        }
        color_transform(ref f, ref frame, ref g, ref boxes, N, N - 1, Color.Orange, "남은 인덱스는 자동으로 정렬된 상태");

        for (int j = 0; j < 20; j++) save_frame(f++, ref frame, ref g, ref boxes, N, "정렬 완료");
    }

    public void Insertion_Sort(ref int[] arr)
    {
        int f = 0;

        Bitmap frame = new Bitmap(W, H);
        Graphics g = Graphics.FromImage(frame);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.Clear(Color.White);

        int N = arr.Length;

        Box[] boxes = new Box[N];
        for (int j = 0; j < N; j++)
        {
            int height = (int)((double)(MAX_H - MIN_H) * ((double)arr[j] / (double)N)) + MIN_H;
            boxes[j] = new Box(diff_x * (j + 1), H - 100 - height, MAX_W, height, arr[j]);
        }

        for (int j = 0; j < 20; j++) save_frame(f++, ref frame, ref g, ref boxes, N);

        color_transform(ref f, ref frame, ref g, ref boxes, N, 0, Color.Orange, "첫 번째 인덱스는 자동으로 정렬된 상태");
        for (int j = 1; j < N; j++)
        {
            int sel = arr[j];
            color_transform(ref f, ref frame, ref g, ref boxes, N, j, Color.OrangeRed, j + " 인덱스를 선택");
            pos_transform(ref f, ref frame, ref g, ref boxes, N, j, N, j + " 인덱스를 선택");
            int i = j;
            while (--i >= 0)
            {
                Color Original_C = boxes[i].clr;
                color_transform(ref f, ref frame, ref g, ref boxes, N, i, Color.LightSlateGray, i + " 인덱스 탐색");
                if (sel < arr[i])
                {
                    arr[i + 1] = arr[i];
                    pos_transform(ref f, ref frame, ref g, ref boxes, N, i, i + 1, i + " 인덱스를 이동");
                    color_transform(ref f, ref frame, ref g, ref boxes, N, i, Original_C, i + " 인덱스를 이동");
                    swap_box(ref boxes, i, i + 1);
                }
                else
                {
                    color_transform(ref f, ref frame, ref g, ref boxes, N, i, Original_C, i + " 인덱스가 선택한 인덱스 보다 더 작음");
                    break;
                }
            }
            arr[i + 1] = sel;
            pos_transform(ref f, ref frame, ref g, ref boxes, N, i + 1, i + 1, "선택한 인덱스를 " + (i + 1) + " 인덱스에 삽입");
            color_transform(ref f, ref frame, ref g, ref boxes, N, i + 1, Color.Orange, (i + 1) + " 인덱스 정렬 완료");
        }
        
        for (int j = 0; j < 20; j++) save_frame(f++, ref frame, ref g, ref boxes, N, "정렬 완료");
    }

    private void Merge_Background(ref Bitmap frame, ref Graphics g, int N)
    {
        Merge_Background_(ref frame, ref g, 0, N - 1, 1, 0);
    }

    private void Merge_Background_(ref Bitmap frame, ref Graphics g, int s, int e, int L, int cx)
    {
        if (s >= e)
        {
            if (s == e) g.DrawRectangle(new Pen(Color.Black, 5), cx + MAX_W * (s + 5), H - MIN_H - diff_y * (10 - L + 1), MAX_W, MIN_H);
            return;
        }

        for (int j = s; j <= e; j++)
        {
            g.DrawRectangle(new Pen(Color.Black, 5), cx + MAX_W * (j + 5), H - MIN_H - diff_y * (10 - L + 1), MAX_W, MIN_H);
        }

        int x = (cx + MAX_W * (s + 5) + cx + MAX_W * (e + 5) + MAX_W) / 2;

        int mid = (s + e + 1) / 2;
        Merge_Background_(ref frame, ref g, s, mid - 1, L + 1, cx - diff_x / L);
        int x2 = (cx - diff_x / L + MAX_W * (s + 5) + cx - diff_x / L + MAX_W * (mid - 1 + 5) + MAX_W) / 2;
        g.DrawLine(new Pen(Color.Black, 3), x, H - MIN_H - diff_y * (10 - L + 1) + MIN_H, x2, H - MIN_H - diff_y * (10 - L));

        Merge_Background_(ref frame, ref g, mid, e, L + 1, cx + diff_x / L);
        x2 = (cx + diff_x / L + MAX_W * (mid + 5) + cx + diff_x / L + MAX_W * (e + 5) + MAX_W) / 2;
        g.DrawLine(new Pen(Color.Black, 3), x, H - MIN_H - diff_y * (10 - L + 1) + MIN_H, x2, H - MIN_H - diff_y * (10 - L));
    }

    public void Merge_Sort(ref int[] arr)
    {
        int f = 0;

        Bitmap frame = new Bitmap(W, H);
        Graphics g = Graphics.FromImage(frame);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.Clear(Color.White);

        int N = arr.Length;

        Box[] boxes = new Box[N];
        for (int j = 0; j < N; j++)
        {
            int color = (int)((double)255 * ((double)arr[j] / (double)N));
            boxes[j] = new Box(MAX_W * (j + 5), H - MIN_H - diff_y * 10, MAX_W, MIN_H, arr[j], Color.FromArgb(255, color, 100, 100));
        }

        for (int j = 0; j < 20; j++) save_frame(f++, ref frame, ref g, ref boxes, N);

        Merge_Sort_(ref arr, 0, N - 1, ref f, ref frame, ref g, ref boxes, N, 1, 0);

        for (int j = 0; j < 20; j++) save_frame(f++, ref frame, ref g, ref boxes, N, "정렬 완료");
    }
    private void Merge_Sort_(ref int[] arr, int s, int e, ref int f, ref Bitmap frame, ref Graphics g, ref Box[] boxes, int N, int L, int cx)
    {
        if (s >= e)
        {
            if (s == e) move_transform(ref f, ref frame, ref g, ref boxes, N, s, cx + MAX_W * (s + 5), H - MIN_H - diff_y * (10 - L + 1), "구간 [" + s + ", " + e + "] 탐색");
            return;
        }

        for (int j = s; j <= e; j++)
        {
            move_transform(ref f, ref frame, ref g, ref boxes, N, j, cx + MAX_W * (j + 5), H - MIN_H - diff_y * (10 - L + 1), "구간 [" + s + ", " + e + "] 탐색");
        }

        int mid = (s + e + 1) / 2;
        Merge_Sort_(ref arr, s, mid - 1, ref f, ref frame, ref g, ref boxes, N, L + 1, cx - diff_x / L);
        Merge_Sort_(ref arr, mid, e, ref f, ref frame, ref g, ref boxes, N, L + 1, cx + diff_x / L);

        //Merge
        int[] t = new int[e - s + 1];
        Box[] tbox = new Box[e - s + 1];
        int p1 = 0, p2 = 0;
        while (s + p1 <= mid - 1 || mid + p2 <= e)
        {
            if (s + p1 <= mid - 1 && mid + p2 <= e)
            {
                if (arr[s + p1] < arr[mid + p2])
                {
                    t[p1 + p2] = arr[s + p1];
                    int j = s + p1 + p2;
                    move_transform(ref f, ref frame, ref g, ref boxes, N, s + p1, cx + MAX_W * (j + 5), H - MIN_H - diff_y * (10 - L + 1), "구간 [" + s + ", " + e + "] 에서 " + (s + p1) + " 인덱스 병합(Merge)");
                    tbox[p1 + p2] = new Box(boxes[s + p1].x, boxes[s + p1].y, boxes[s + p1].width, boxes[s + p1].height, boxes[s + p1].v, boxes[s + p1].clr);
                    p1++;
                }
                else
                {
                    t[p1 + p2] = arr[mid + p2];
                    int j = s + p1 + p2;
                    move_transform(ref f, ref frame, ref g, ref boxes, N, mid + p2, cx + MAX_W * (j + 5), H - MIN_H - diff_y * (10 - L + 1), "구간 [" + s + ", " + e + "] 에서 " + (mid + p2) + " 인덱스 병합(Merge)");
                    tbox[p1 + p2] = new Box(boxes[mid + p2].x, boxes[mid + p2].y, boxes[mid + p2].width, boxes[mid + p2].height, boxes[mid + p2].v, boxes[mid + p2].clr);
                    p2++;
                }
            }
            else if (s + p1 <= mid - 1)
            {
                t[p1 + p2] = arr[s + p1];
                int j = s + p1 + p2;
                move_transform(ref f, ref frame, ref g, ref boxes, N, s + p1, cx + MAX_W * (j + 5), H - MIN_H - diff_y * (10 - L + 1), "구간 [" + s + ", " + e + "] 에서 " + (s + p1) + " 인덱스 병합(Merge)");
                tbox[p1 + p2] = new Box(boxes[s + p1].x, boxes[s + p1].y, boxes[s + p1].width, boxes[s + p1].height, boxes[s + p1].v, boxes[s + p1].clr);
                p1++;
            }
            else
            {
                t[p1 + p2] = arr[mid + p2];
                int j = s + p1 + p2;
                move_transform(ref f, ref frame, ref g, ref boxes, N, mid + p2, cx + MAX_W * (j + 5), H - MIN_H - diff_y * (10 - L + 1), "구간 [" + s + ", " + e + "] 에서 " + (mid + p2) + " 인덱스 병합(Merge)");
                tbox[p1 + p2] = new Box(boxes[mid + p2].x, boxes[mid + p2].y, boxes[mid + p2].width, boxes[mid + p2].height, boxes[mid + p2].v, boxes[mid + p2].clr);
                p2++;
            }
        }

        for (int j = 0; j < p1 + p2; j++)
        {
            arr[s + j] = t[j];
            boxes[s + j] = new Box(tbox[j].x, tbox[j].y, tbox[j].width, tbox[j].height, tbox[j].v, tbox[j].clr);
        }
    }

    private void Heap_Background(ref Bitmap frame, ref Graphics g, int N)
    {
        Heap_Background_(ref frame, ref g, N, 0, 1, 0);
    }

    private void Heap_Background_(ref Bitmap frame, ref Graphics g, int N, int i, int L, int cx)
    {
        g.DrawRectangle(new Pen(Color.Black, 5), cx + MAX_W * 10, H - MIN_H - diff_y * (10 - L + 1) + 150, MAX_W, MIN_H);

        int x = cx + MAX_W * 10 + MAX_W / 2;

        int x2 = cx - diff_x / (1 << L) + MAX_W * 10 + MAX_W / 2;
        if ((i + 1) * 2 - 1 < N)
        {
            Heap_Background_(ref frame, ref g, N, (i + 1) * 2 - 1, L + 1, cx - diff_x / (1 << L));
            g.DrawLine(new Pen(Color.Black, 3), x, H - MIN_H - diff_y * (10 - L + 1) + MIN_H + 150, x2, H - MIN_H - diff_y * (10 - L) + 150);
        }
        x2 = cx + diff_x / (1 << L) + MAX_W * 10 + MAX_W / 2;
        if ((i + 1) * 2 < N)
        {
            Heap_Background_(ref frame, ref g, N, (i + 1) * 2, L + 1, cx + diff_x / (1 << L));
            g.DrawLine(new Pen(Color.Black, 3), x, H - MIN_H - diff_y * (10 - L + 1) + MIN_H + 150, x2, H - MIN_H - diff_y * (10 - L) + 150);
        }
    }

    private void Heap_set_boxes(ref Box[] boxes, int N, int i, int L, int cx)
    {
        boxes[N + i].x = cx + MAX_W * 10;
        boxes[N + i].y = H - MIN_H - diff_y * (10 - L + 1) + 150;
        
        if ((i + 1) * 2 - 1 < N)
            Heap_set_boxes(ref boxes, N, (i + 1) * 2 - 1, L + 1, cx - diff_x / (1 << L));
        if ((i + 1) * 2 < N)
            Heap_set_boxes(ref boxes, N, (i + 1) * 2, L + 1, cx + diff_x / (1 << L));
    }

    public void Heap_Sort(ref int[] arr)
    {
        int f = 0;

        Bitmap frame = new Bitmap(W, H);
        Graphics g = Graphics.FromImage(frame);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.Clear(Color.White);

        int N = arr.Length;

        Box[] boxes = new Box[N * 2];
        for (int j = 0; j < N; j++)
        {
            int color = (int)((double)255 * ((double)arr[j] / (double)N));
            boxes[j] = new Box((MAX_W + 20) * (j + 2), H - MIN_H - diff_y * 10, MAX_W, MIN_H, arr[j], Color.FromArgb(255, color, 100, 100));
            boxes[N + j] = new Box((MAX_W + 20) * (j + 2), H - MIN_H - diff_y * 10, MAX_W, MIN_H, arr[j], Color.FromArgb(255, color, 100, 100));
        }
        Heap_set_boxes(ref boxes, N, 0, 1, 0);

        for (int j = 0; j < 100; j++) save_frame(f++, ref frame, ref g, ref boxes, N);
        
        //BUILD HEAP
        for (int j = N / 2 - 1; j >= 0; j--)
        {
            Color org_clr = boxes[j].clr;
            color_transform(ref f, ref frame, ref g, ref boxes, N, new int[2]{ j, N + j }, new Color[2] { Color.Orange, Color.Orange }, j + " 인덱스 Max-Heapify (Build Heap 과정)");
            int i = MAX_Heapify(ref frame, ref g, ref boxes, ref f, ref arr, N, N, j, "(Build Heap 과정의 Max-Heapify)");
            color_transform(ref f, ref frame, ref g, ref boxes, N, new int[2] { i, N + i }, new Color[2] { org_clr, org_clr }, i + " 인덱스로 Max-Heapify 완료 (Build Heap 과정)");
        }

        //EXTRACT MAX
        for (int j = 0; j < N - 1; j++)
        {
            Color org_clr1 = boxes[0].clr, org_clr2 = boxes[N - 1 - j].clr;
            color_transform(ref f, ref frame, ref g, ref boxes, N, new int[4] { 0, N + 0, N - 1 - j, N + N - 1 - j }, new Color[4] { Color.Red, Color.Red, Color.Orange, Color.Orange }, 0 + " 인덱스 최댓값으로 추출 (Extract-Max 과정)");

            swap_transform(ref f, ref frame, ref g, ref boxes, N, new int[2] { 0, N + 0 }, new int[2] { N - 1 - j, N + N - 1 - j }, 0 + " 인덱스 최댓값으로 추출 (Extract-Max 과정)");
            
            swap_box(ref boxes, 0, N - 1 - j);
            swap_box(ref boxes, N + 0, N + N - 1 - j);
            color_transform(ref f, ref frame, ref g, ref boxes, N, new int[4] { 0, N + 0, N - 1 - j, N + N - 1 - j }, new Color[4] { Color.Orange, Color.Orange, org_clr1, Color.DarkRed }, 0 + " 인덱스 Max-Heapify (Extract-Max 과정)");

            Swap(ref arr, 0, N - 1 - j);
            int i = MAX_Heapify(ref frame, ref g, ref boxes, ref f, ref arr, N, N - j - 1, 0, "(Extract-Max 과정의 Max-Heapify)");
            
            color_transform(ref f, ref frame, ref g, ref boxes, N, new int[2] { i, N + i }, new Color[2] { org_clr2, org_clr2 }, i + " 인덱스로 Max-Heapify 완료 (Extract-Max 과정)");
        }
        color_transform(ref f, ref frame, ref g, ref boxes, N, N, Color.DarkRed, "마지막 원소는 자동으로 정렬 완료");

        for (int j = 0; j < 100; j++) save_frame(f++, ref frame, ref g, ref boxes, N, "정렬 완료");
    }
    private int MAX_Heapify(ref Bitmap frame, ref Graphics g, ref Box[] boxes, ref int f, ref int[] arr, int N, int nN, int i, string str = "")
    {
        int left = (i + 1) * 2 - 1;
        int right = (i + 1) * 2;
        if ((left < nN && arr[i] < arr[left]) || (right < nN && arr[i] < arr[right]))
        {
            if ((left < nN && arr[i] < arr[left]) && (right < nN && arr[i] < arr[right]))
            {
                if (arr[left] > arr[right])
                {
                    swap_transform(ref f, ref frame, ref g, ref boxes, N, new int[2]{i, N + i}, new int[2]{left, N + left}, "왼쪽 자식과 swap " + str);
                    swap_box(ref boxes, i, left);
                    swap_box(ref boxes, N + i, N + left);
                    Swap(ref arr, i, left);
                    return MAX_Heapify(ref frame, ref g, ref boxes, ref f, ref arr, N, nN, left, str);
                }
                else
                {
                    swap_transform(ref f, ref frame, ref g, ref boxes, N, new int[2] { i, N + i }, new int[2] { right, N + right }, "오른쪽 자식과 swap " + str);
                    swap_box(ref boxes, i, right);
                    swap_box(ref boxes, N + i, N + right);
                    Swap(ref arr, i, right);
                    return MAX_Heapify(ref frame, ref g, ref boxes, ref f, ref arr, N, nN, right, str);
                }
            }
            else if (left < N && arr[i] < arr[left])
            {
                swap_transform(ref f, ref frame, ref g, ref boxes, N, new int[2] { i, N + i }, new int[2] { left, N + left }, "왼쪽 자식과 swap " + str);
                swap_box(ref boxes, i, left);
                swap_box(ref boxes, N + i, N + left);
                Swap(ref arr, i, left);
                return MAX_Heapify(ref frame, ref g, ref boxes, ref f, ref arr, N, nN, left, str);
            }
            else
            {
                swap_transform(ref f, ref frame, ref g, ref boxes, N, new int[2] { i, N + i }, new int[2] { right, N + right }, "오른쪽 자식과 swap " + str);
                swap_box(ref boxes, i, right);
                swap_box(ref boxes, N + i, N + right);
                Swap(ref arr, i, right);
                return MAX_Heapify(ref frame, ref g, ref boxes, ref f, ref arr, N, nN, right, str);
            }
        }
        return i;
    }

    public void Quick_Sort(ref int[] arr)
    {
        int N = arr.Length;
        Quick_Sort_(ref arr, 0, N - 1);
    }
    private void Quick_Sort_(ref int[] arr, int s, int e)
    {
        if (s >= e)
            return;

        int pivot = e;
        Swap(ref arr, pivot, rnd.Next(s, e + 1)); //Randomized Sort
        int i = s, j = e - 1;
        int min = arr[pivot], max = arr[pivot]; //모든 숫자가 동일한 경우

        for (int k = s; k < e; k++)
        {
            min = Math.Min(min, arr[i]);
            max = Math.Max(max, arr[i]);

            if (arr[i] < arr[pivot])
                i++;
            else
            {
                Swap(ref arr, i, j);
                j--;
            }
        }
        Swap(ref arr, pivot, j + 1);

        if (min == max) return;
        Quick_Sort_(ref arr, s, j);
        Quick_Sort_(ref arr, j + 1, e);
    }

    private void Swap(ref int[] arr, int i, int j)
    {
        int t = arr[i];
        arr[i] = arr[j];
        arr[j] = t;
    }
}