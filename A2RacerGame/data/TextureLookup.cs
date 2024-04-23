class TextureLookup
{
    public static Dictionary<string, string> textureMap =
        new()
        {
            { "adac", "racer02" }, // done
            { "a_afslag", "a_trffc1" }, // not done
            { "a_afslg1", "a_trffc1" }, // done
            { "a_afslg2", "a_trffc1" }, // done
            { "a_bannrs", "a_trffc1" }, // done
            { "a_bgbrg1", "algemeen" }, // done
            { "a_bgbrg2", "algemeen" }, // done
            { "a_bgbrg3", "algemeen" }, // done
            { "a_bhal1", "algemeen" }, // done
            { "a_bhal2", "algemeen" }, // done
            { "a_blinks", "algemeen" }, // not done
            { "a_bord2b", "a_trffc1" }, // done
            { "a_bord3b", "a_trffc1" }, // done
            { "a_bpilbp", "algemeen" }, // done
            { "a_bpilr1", "algemeen" }, // done
            { "a_bpilr2", "algemeen" }, // done
            { "a_brdrij", "algemeen" }, // done
            { "a_brest", "a_wgwerk" }, // done
            { "a_brugje", "a_brugje" }, // not done
            { "a_check2", "a_trffc1" }, // done
            { "a_check3", "a_trffc1" }, // done
            { "a_check4", "a_trffc1" }, // done
            { "a_checkp", "a_trffc1" }, // done
            { "a_cnstr0", "a_wgwerk" }, // done
            { "a_cnstr1", "a_wgwerk" }, // done
            { "a_cnstr2", "a_wgwerk" }, // done
            { "a_cnstr3", "a_wgwerk" }, // done
            { "a_elmast", "algemeen" }, // done
            { "a_fltsrp", "a_wgwerk" }, // done
            { "a_flyov", "a_flyov" }, // not done
            { "a_hal", "algemeen" }, // done
            { "a_hobbel", "a_hobbel" }, // not done
            { "a_inhaal", "a_trffc1" }, // done
            { "a_invg1", "c_trffc1" }, // done
            { "a_kant1", "algemeen" }, // not done
            { "a_kantor", "algemeen" }, // not done
            { "a_keet", "a_wgwerk" }, // done
            { "a_koe1", "algemeen" }, // done
            { "a_koe2", "algemeen" }, // done
            { "a_laqua1", "algemeen" }, // done
            { "a_laqua2", "algemeen" }, // done
            { "a_lntrn0", "a_trffc1" }, // done
            { "a_lntrn1", "a_trffc1" }, // done
            { "a_lntrn2", "a_trffc1" }, // done
            { "a_lpijl0", "a_wgwerk" }, // done
            { "a_lpijl1", "a_wgwerk" }, // done
            { "a_max50s", "a_trffc1" }, // done
            { "a_max80s", "a_trffc1" }, // done
            { "a_mens1", "a_people" }, // done
            { "a_mens10", "a_people" }, // done
            { "a_mens11", "a_people" }, // done
            { "a_mens2", "a_people" }, // done
            { "a_mens3", "a_people" }, // done
            { "a_mens4", "a_people" }, // done
            { "a_mens5", "a_people" }, // done
            { "a_mens6", "a_people" }, // done
            { "a_mens7", "a_people" }, // done
            { "a_mens8", "a_people" }, // done
            { "a_mens9", "a_people" }, // done
            { "a_molen", "algemeen" }, // done
            { "a_mx120s", "a_trffc1" }, // done
            { "a_mxe", "a_trffc1" }, // done
            { "a_pijl0", "a_wgwerk" }, // done
            { "a_pijl1", "a_wgwerk" }, // done
            { "a_pilon1", "a_wgwerk" }, // done
            { "a_pratpl", "a_wgwerk" }, // done
            { "a_radar", "a_radar" }, // done
            { "a_rczuil", "algemeen" }, // done
            { "a_rdblk", "a_wgwerk" }, // done
            { "a_recl01", "a_recl01" }, // not done
            { "a_recl02", "a_recl02" }, // not done
            { "a_recl03", "a_recl03" }, // not done
            { "a_restau", "algemeen" }, // done
            { "a_shadow", "a_shadow" }, // done
            { "a_sprln", "algemeen" }, // done
            { "a_sprln2", "algemeen" }, // done
            { "a_tank1", "a_tank1" }, // not done
            { "a_tank2", "a_tank2" }, // not done
            { "a_tank3", "a_tank3" }, // not done
            { "a_tanken", "a_wgwerk" }, // done
            { "a_tegenl", "a_trffc1" }, // done
            { "a_vdct11", "a_vdct11" }, // not done
            { "a_vdct21", "a_vdct21" }, // not done
            { "a_versm", "a_trffc1" }, // done
            { "a_versm2", "a_trffc1" }, // done
            { "a_viadct", "algemeen" }, // done
            { "a_viadof", "algemeen" }, // done
            { "a_vidctj", "algemeen" }, // done
            { "a_vidsp1", "a_trffc1" }, // done
            { "a_vidsp2", "a_trffc1" }, // done
            { "a_vliegt", "algemeen" }, // done
            { "a_vngrail", "algemeen" }, // done
            { "a_wndmln", "algemeen" }, // done
            { "bus", "bus" }, // not done
            { "c_banrsf", "c_trffc1" }, // done
            { "c_blinks", "c_blinks" }, // not done
            { "c_blox", "c_trffc1" }, // done
            { "c_bovenl", "c_trffc1" }, // done
            { "c_brchts", "c_brchts" }, // not done
            { "c_busbrd", "c_trffc1" }, // done
            { "c_bushok", "c_wgwerk" }, // done
            { "c_cnstr0", "c_trffc1" }, // done
            { "c_cnstr1", "c_trffc1" }, // done
            { "c_cnstr2", "c_trffc1" }, // done
            { "c_cnstr3", "c_trffc1" }, // done
            { "c_cnstr4", "c_trffc1" }, // done
            { "c_cnstr5", "c_trffc1" }, // done
            { "c_fltsrp", "c_trffc1" }, // done
            { "c_hek1", "c_trffc1" }, // done
            { "c_hek2", "c_trffc1" }, // done
            { "c_hek3", "c_trffc1" }, // done
            { "c_hek4", "c_trffc1" }, // done
            { "c_hek5", "c_trffc1" }, // done
            { "c_hek6", "c_trffc1" }, // done
            { "c_heli", "c_wgwerk" }, // done
            { "c_hoop", "c_wgwerk" }, // done
            { "c_htpaal", "c_trffc1" }, // done
            { "c_invali", "c_trffc1" }, // done
            { "c_invg1", "c_trffc1" }, // done
            { "c_keet", "c_wgwerk" }, // done
            { "c_lblade", "c_wgwerk" }, // done
            { "c_lntrn0", "a_trffc1" }, // done
            { "c_max60s", "c_trffc1" }, // done
            { "c_max80s", "c_trffc1" }, // done
            { "c_mens1", "c_people" }, // done
            { "c_mens10", "c_people" }, // done
            { "c_mens11", "c_people" }, // done
            { "c_mens2", "c_people" }, // done
            { "c_mens3", "c_people" }, // done
            { "c_mens4", "c_people" }, // done
            { "c_mens5", "c_people" }, // done
            { "c_mens6", "c_people" }, // done
            { "c_mens7", "c_people" }, // done
            { "c_mens8", "c_people" }, // done
            { "c_mens9", "c_people" }, // done
            { "c_midpyl", "c_trffc1" }, // done
            { "c_mx120s", "c_trffc1" }, // done
            { "c_mxe", "c_trffc1" }, // done
            { "c_pijl0", "c_wgwerk" }, // done
            { "c_pijl1", "c_wgwerk" }, // done
            { "c_pilon1", "c_trffc1" }, // done
            { "c_pompen", "c_thouse" }, // done
            { "c_radar", "c_radar" }, // done
            { "c_rblok", "c_trffc1" }, // done
            { "c_sblade", "c_wgwerk" }, // done
            { "c_shadow", "c_shadow" }, // done
            { "c_stoplg", "c_trffc1" }, // done
            { "c_stoplo", "c_trffc1" }, // done
            { "c_stoplr", "c_trffc1" }, // done
            { "c_stoplz", "c_stoplz" }, // not done
            { "c_stplg2", "c_trffc1" }, // done
            { "c_stplg3", "c_stplg3" }, // not done
            { "c_stplo2", "c_trffc1" }, // done
            { "c_stplo3", "c_stplo3" }, // not done
            { "c_stplr2", "c_trffc1" }, // done
            { "c_stplr3", "c_stplr3" }, // not done
            { "c_stplz2", "c_stplz2" }, // not done
            { "c_stplz3", "c_stplz3" }, // not done
            { "c_tanken", "c_tanken" }, // not done
            { "c_tegenl", "c_trffc1" }, // done
            { "c_thouse", "c_thouse" }, // done
            { "c_tram", "cars02" }, // done
            { "c_trein", "cars01" }, // done
            { "c_versm", "c_trffc1" }, // done
            { "c_versm2", "c_trffc1" }, // done
            { "c_vstopl", "c_trffc1" }, // done
            { "c_wblok", "c_trffc1" }, // done
            { "default", "default" }, // not done
            { "e00_adam", "c_trffc1" },
            { "e00_beur", "e00_beur" },
            { "e00_bom1", "e00_bomn" },
            { "e00_bom2", "e00_bomn" },
            { "e00_bom3", "e00_bomn" },
            { "e00_bom4", "e00_bomn" },
            { "e00_brug", "e00_gras" },
            { "e00_col1", "e00_weg" },
            { "e00_col2", "e00_weg" },
            { "e00_col3", "e00_weg" },
            { "e00_conc", "e00_conc" },
            { "e00_cs", "e00_cs" },
            { "e00_geb1", "e00_gra2" },
            { "e00_lan2", "e00_lan2" },
            { "e00_lanl", "c_trffc1" },
            { "e00_lanr", "C_trffc1" },
            { "e00_lant", "c_trffc1" },
            { "e00_luth", "e00_luth" },
            { "e00_mid1", "e00_gras" },
            { "e00_mid2", "e00_gras" },
            { "e00_mid3", "e00_gras" },
            { "e00_monu", "e00_monu" },
            { "e00_munt", "e00_munt" },
            { "e00_ncls", "e00_ncls" },
            { "e00_newk", "e00_newk" },
            { "e00_oudk", "e00_oudk" },
            { "e00_pals", "e00_pals" },
            { "e00_rec1", "e00_bomn" },
            { "e00_rec2", "e00_bomn" },
            { "e00_rec3", "e00_bomn" },
            { "e00_rec4", "e00_bomn" },
            { "e00_rec5", "e00_bomn" },
            { "e00_rec6", "e00_bomn" },
            { "e00_rec7", "e00_bomn" },
            { "e00_rec8", "e00_bomn" },
            { "e00_rec9", "e00_bomn" },
            { "e00_reca", "e00_bomn" },
            { "e00_recb", "e00_bomn" },
            { "e00_recc", "e00_bomn" },
            { "e00_recd", "e00_bomn" },
            { "e00_rece", "e00_bomn" },
            { "e00_rond", "e00_rond" },
            { "e00_ryks", "e00_ryks" },
            { "e00_uitl", "e00_gras" },
            { "e00_uitr", "e00_gras" },
            { "e00_waag", "e00_waag" },
            { "e00_zij2", "e00_zij2" },
            { "e00_zijs", "e00_gra2" },
            { "e00_zuil", "e00_zuil" },
            { "e01_aqua", "e01_aqua" },
            { "e01_hoti", "e01_hoti" },
            { "e01_huis", "e01_huis" },
            { "e01_kan1", "e01_kan1" },
            { "e01_kan2", "e01_kan2" },
            { "e01_kant", "e01_kant" },
            { "e01_kerk", "e01_kerk" },
            { "e01_niss", "e01_niss" },
            { "e01_plei", "e01_plei" },
            { "e01_rest", "e01_rest" },
            { "e01_schi", "e01_schi" },
            { "e01_sc~1", "e01_sc~1" },
            { "e01_skyl", "e01_skyl" },
            { "e01_sony", "e01_sony" },
            { "e01_stop", "e01_stop" },
            { "e01_vial", "e01_vial" },
            { "e01_vias", "e01_vias" },
            { "e01_viav", "e01_viav" },
            { "e01_vsch", "e01_vsch" },
            { "e01_witk", "e01_witk" },
            { "e02_1813", "e02_1813" },
            { "e02_blok", "e02_blok" },
            { "e02_bom1", "e02_bom1" },
            { "e02_bom2", "e02_bom2" },
            { "e02_bom3", "e02_bom3" },
            { "e02_bom4", "e02_bom4" },
            { "e02_col1", "e02_col1" },
            { "e02_col2", "e02_col2" },
            { "e02_col3", "e02_col3" },
            { "e02_col4", "e02_col4" },
            { "e02_cs", "e02_cs" },
            { "e02_geb1", "e02_geb1" },
            { "e02_geb2", "e02_geb2" },
            { "e02_grot", "e02_grot" },
            { "e02_hek", "e02_hek" },
            { "e02_hekp", "e02_hekp" },
            { "e02_hs", "e02_hs" },
            { "e02_lan2", "e02_lan2" },
            { "e02_lanl", "e02_lanl" },
            { "e02_lanr", "e02_lanr" },
            { "e02_lant", "e02_lant" },
            { "e02_luif", "e02_luif" },
            { "e02_maur", "e02_maur" },
            { "e02_mid1", "e02_mid1" },
            { "e02_mid2", "e02_mid2" },
            { "e02_mid3", "e02_mid3" },
            { "e02_newk", "e02_newk" },
            { "e02_nord", "e02_nord" },
            { "e02_port", "e02_port" },
            { "e02_radh", "e02_radh" },
            { "e02_recl", "e02_recl" },
            { "e02_resi", "e02_resi" },
            { "e02_ridr", "e02_ridr" },
            { "e02_roto", "e02_roto" },
            { "e02_stad", "e02_stad" },
            { "e02_stal", "e02_stal" },
            { "e02_uitl", "e02_uitl" },
            { "e02_uitr", "e02_uitr" },
            { "e02_viad", "e02_viad" },
            { "e02_vred", "e02_vred" },
            { "e02_vrom", "e02_vrom" },
            { "e02_wbin", "e02_wbin" },
            { "e02_zij2", "e02_zij2" },
            { "e02_zijs", "e02_zijs" },
            { "e03_bdrf", "e03_bdrf" },
            { "e03_fini", "e03_fini" },
            { "e03_hhus", "e03_hhus" },
            { "e03_hus1", "e03_hus1" },
            { "e03_ikea", "e03_ikea" },
            { "e03_inds", "e03_inds" },
            { "e03_jr50", "e03_jr50" },
            { "e03_kntr", "e03_kntr" },
            { "e03_ktwt", "e03_ktwt" },
            { "e03_merc", "e03_merc" },
            { "e03_motbrk", "e03_motbrk" },
            { "e03_ntnd", "e03_ntnd" },
            { "e03_rdmv", "e03_rdmv" },
            { "e03_reac", "e03_reac" },
            { "e03_vnlw", "e03_vnlw" },
            { "e03_wknt", "e03_wknt" },
            { "e04_apsl", "e04_apsl" },
            { "e04_bhof", "e04_bhof" },
            { "e04_blok", "e04_blok" },
            { "e04_bom1", "e04_bom1" },
            { "e04_bom2", "e04_bom2" },
            { "e04_bom3", "e04_bom3" },
            { "e04_bom4", "e04_bom4" },
            { "e04_bom5", "e04_bom5" },
            { "e04_bom6", "e04_bom6" },
            { "e04_bom7", "e04_bom7" },
            { "e04_bos5", "e04_bos5" },
            { "e04_bul1", "e04_bul1" },
            { "e04_bul2", "e04_bul2" },
            { "e04_bul3", "e04_bul3" },
            { "e04_col1", "e04_col1" },
            { "e04_col2", "e04_col2" },
            { "e04_cs", "e04_cs" },
            { "e04_delf", "e04_delf" },
            { "e04_dkzt", "e04_dkzt" },
            { "e04_dykz", "e04_dykz" },
            { "e04_eigl", "e04_eigl" },
            { "e04_eigv", "e04_eigv" },
            { "e04_emst", "e04_emst" },
            { "e04_eras", "e04_eras" },
            { "e04_euro", "e04_euro" },
            { "e04_geb1", "e04_geb1" },
            { "e04_geb2", "e04_geb2" },
            { "e04_hahn", "e04_hahn" },
            { "e04_jkor", "e04_jkor" },
            { "e04_kdom", "e04_kdom" },
            { "e04_koor", "e04_koor" },
            { "e04_lan2", "e04_lan2" },
            { "e04_lan3", "e04_lan3" },
            { "e04_lan4", "e04_lan4" },
            { "e04_lanl", "e04_lanl" },
            { "e04_lanr", "e04_lanr" },
            { "e04_lant", "e04_lant" },
            { "e04_mart", "e04_mart" },
            { "e04_mid1", "e04_mid1" },
            { "e04_mid2", "e04_mid2" },
            { "e04_mid3", "e04_mid3" },
            { "e04_potl", "e04_potl" },
            { "e04_recl", "e04_recl" },
            { "e04_reon", "e04_reon" },
            { "e04_rij", "e04_rij" },
            { "e04_rij2", "e04_rij2" },
            { "e04_rij3", "e04_rij3" },
            { "e04_roto", "e04_roto" },
            { "e04_sevr", "e04_sevr" },
            { "e04_stdh", "e04_stdh" },
            { "e04_trop", "e04_trop" },
            { "e04_uitl", "e04_uitl" },
            { "e04_uitr", "e04_uitr" },
            { "e04_via2", "e04_via2" },
            { "e04_viad", "e04_viad" },
            { "e04_viad_try1", "e04_viad_try1" },
            { "e04_viat", "e04_viat" },
            { "e04_wdr", "e04_wdr" },
            { "e04_werf", "e04_werf" },
            { "e04_wilb", "e04_wilb" },
            { "e04_wilb2", "e04_wilb2" },
            { "e04_wlrf", "e04_wlrf" },
            { "e04_wtc", "e04_wtc" },
            { "e04_zij2", "e04_zij2" },
            { "e04_zijs", "e04_zijs" },
            { "e05_alx1", "e05_alx1" },
            { "e05_alx4", "e05_alx4" },
            { "e05_alx5", "e05_alx5" },
            { "e05_aren", "e05_aren" },
            { "e05_bgbr", "e05_bgbr" },
            { "e05_brgj", "e05_brgj" },
            { "e05_bruk", "e05_bruk" },
            { "e05_dsco", "e05_dsco" },
            { "e05_elek", "e05_elek" },
            { "e05_ptdk", "e05_ptdk" },
            { "e05_rcl1", "e05_rcl1" },
            { "e05_recl", "e05_recl" },
            { "ltr_0", "s_letter" }, // done
            { "ltr_1", "s_letter" }, // done
            { "ltr_2", "s_letter" }, // done
            { "ltr_3", "s_letter" }, // done
            { "ltr_4", "s_letter" }, // done
            { "ltr_5", "s_letter" }, // done
            { "ltr_6", "s_letter" }, // done
            { "ltr_7", "s_letter" }, // done
            { "ltr_8", "s_letter" }, // done
            { "ltr_9", "s_letter" }, // done
            { "ltr_a", "s_letter" }, // done
            { "ltr_aanh", "s_letter" }, // done
            { "ltr_ae", "s_letter" }, // done
            { "ltr_b", "s_letter" }, // done
            { "ltr_c", "s_letter" }, // done
            { "ltr_d", "s_letter" }, // done
            { "ltr_e", "s_letter" }, // done
            { "ltr_f", "s_letter" }, // done
            { "ltr_g", "s_letter" }, // done
            { "ltr_h", "s_letter" }, // done
            { "ltr_i", "s_letter" }, // done
            { "ltr_is", "s_letter" }, // done
            { "ltr_j", "s_letter" }, // done
            { "ltr_k", "s_letter" }, // done
            { "ltr_l", "s_letter" }, // done
            { "ltr_line", "s_letter" }, // done
            { "ltr_m", "s_letter" }, // done
            { "ltr_maal", "s_letter" }, // done
            { "ltr_n", "s_letter" }, // done
            { "ltr_o", "s_letter" }, // done
            { "ltr_oe", "s_letter" }, // done
            { "ltr_p", "s_letter" }, // done
            { "ltr_pls", "s_letter" }, // done
            { "ltr_plus", "s_letter" }, // done
            { "ltr_punt", "s_letter" }, // done
            { "ltr_q", "s_letter" }, // done
            { "ltr_r", "s_letter" }, // done
            { "ltr_s", "s_letter" }, // done
            { "ltr_ss", "s_letter" }, // done
            { "ltr_t", "s_letter" }, // done
            { "ltr_u", "s_letter" }, // done
            { "ltr_ue", "s_letter" }, // done
            { "ltr_uitr", "s_letter" }, // done
            { "ltr_v", "s_letter" }, // done
            { "ltr_vrag", "s_letter" }, // done
            { "ltr_w", "s_letter" }, // done
            { "ltr_x", "s_letter" }, // done
            { "ltr_y", "s_letter" }, // done
            { "ltr_z", "s_letter" }, // done
            { "mpolizei", "cars01" }, // done
            { "n_astra", "cars02" }, // done
            { "n_bestel", "cars03" }, // done
            { "n_bus", "cars02" }, // done
            { "n_calibr", "cars01" }, // done
            { "n_golf", "cars02" }, // done
            { "n_polo", "cars03" }, // done
            { "n_vrtaut", "cars03" }, // done
            { "n_wals", "cars01" }, // done
            { "r_astra", "racer01" }, // done
            { "r_beetle", "racer02" }, // done
            { "r_bmw", "racer01" }, // done
            { "r_clk", "racer01" }, // done
            { "r_diablo", "racer01" }, // done
            { "r_porch", "racer02" }, // done
            { "r_porche", "racer02" }, // done
            { "r_trabbi", "racer02" }, // done
            { "r_vector", "r_vector" }, // not done
            { "s_licht", "s_sfx" }, // done
            { "s_shadow", "s_shadow" }, // done
            { "vpolizei", "vpolizei" }, // not done
            { "vracht", "vracht" }, // not done
            { "wiel", "cars01" }, // done
        };

    public static string GetTextureForModel(string model)
    {
        if (textureMap.ContainsKey(model.ToLower()))
            return textureMap[model.ToLower()];
        return "";
    }
}
